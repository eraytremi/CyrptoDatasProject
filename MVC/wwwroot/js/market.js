
$(document).ready(function () {

    var currentTickers = {};
    var currentModalTicker = null;

    // WebSocket ba�lant�s�
    const socket = new WebSocket('wss://stream.binance.com:9443/ws/!ticker@arr');
    var tickers;
    socket.onmessage = function (event) {
        tickers = JSON.parse(event.data);
        updateTableData(tickers);



        tickers.forEach(function (ticker) {
            // Sadece belirli hedef para birimleri i�in i�le
            var targetCurrency = ticker.s.substr(-4); // Paritenin son 4 karakterini al
            var isTargetCurrency = ['USDT'].includes(targetCurrency);

           

            if (isTargetCurrency) {
                // Bu k�s�m yukar�daki kodla ayn�
                currentTickers[ticker.s] = ticker;
                var existingRow = dataTable.row('#' + ticker.s);

                if (existingRow.length) {
                    existingRow.data([
                        ticker.s,
                        ticker.p,
                        ticker.P,
                        ticker.c
                    ]).draw(false);
                } else {
                    dataTable.row.add([
                        ticker.s,
                        ticker.p,
                        ticker.P,
                        ticker.c
                    ]).node().id = ticker.s;
                    dataTable.draw(false);
                }

                // E�er modal a��ksa ve g�ncellenen ticker, modalda g�sterilen ticker ise
                if ($('#dataModal').hasClass('show') && currentModalTicker === ticker.s) {
                    updateModalData(ticker);
                }
            }
        });
    };


    function updateTableRowColor(row, oldData, newData) {
        var cells = row.nodes().to$().find('td');
        for (var i = 0; i < newData.length; i++) {
            if (parseFloat(newData[i]) > parseFloat(oldData[i])) {
                $(cells[i]).addClass('increase').removeClass('decrease');
            } else if (parseFloat(newData[i]) < parseFloat(oldData[i])) {
                $(cells[i]).addClass('decrease').removeClass('increase');
            }
        }
    }

    function updateTableData(tickers) {
        tickers.forEach(function (ticker) {
            var existingRow = dataTable.row('#' + ticker.s);
            if (existingRow.length) {
                var oldData = existingRow.data();
                var newData = [
                    ticker.s, // Sembol
                    ticker.p, // Fiyat De�i�imi
                    ticker.P, // Fiyat De�i�imi (%)
                    ticker.c  // Fiyat
                ];

                // Yeni verilerle sat�r� g�ncelle
                existingRow.data(newData).draw(false);

                // Her h�cre i�in renk g�ncellemesi yap
                updateTableRowColor(existingRow, oldData, newData);
            }
        });
    }



    // Modal i�eri�ini g�ncelle
    function updateModalData(ticker) {
        $('#modalSymbol').text(ticker.s);
        $('#modalLastPrice').text(ticker.c);
    }

    // Miktar giri�i i�in event handler
    $('#marketAmount').on('input', function () {
        var amount = $(this).val();
        var price = parseFloat($('#modalLastPrice').text());
        var total = amount * price;
        $('#marketTotal').text(total.toFixed(2));
    });

    $('#btnBuyMarket').on('click', function () {
        var symbol = $('#modalSymbol').text();
        var count = parseFloat($('#marketAmount').val());
        var price = $('#modalLastPrice').text();
        postBuyMarket(symbol, count, price);
    });

    $('#btnSellMarket').on('click', function () {
        var symbol = $('#modalSymbol').text();
        var count = parseFloat($('#marketAmount').val());
        var price = $('#modalLastPrice').text();
        postSellMarket(symbol, count, price);
    });

    $('#btnBuyLimit').on('click', function () {
        var symbol = $('#modalSymbol').text();
        var limitAmount = parseFloat($('#limitAmount').val());
        var limitPrice = $('#limitPrice').text();
        postBuyLimit(symbol, limitAmount, limitPrice);
    });

    var pendingOrders = [];
    var intervalID;
    //Limit Emir sekmesine ge�ildi�inde ve modal a��ld���nda fiyat� g�ncelle
    function updateLimitPrice() {
        var currentPrice = $('#modalLastPrice').text();
        $('#limitPrice').val(currentPrice);
    }

    // Limit Emir sekmesine ge�i� yap�ld���nda fiyat� g�ncelle
    $('#limit-tab').on('shown.bs.tab', function (e) {
        updateLimitPrice();
    });

    // Modal a��ld���nda fiyat� g�ncelle
    $('#dataModal').on('shown.bs.modal', function (e) {
        if ($('#limit-tab').hasClass('active')) {
            updateLimitPrice();
        }
    });


    // Limit emir Toplam� hesapla ve g�ster
    function updateTotal() {
        var price = parseFloat($('#limitPrice').val());
        var amount = parseFloat($('#limitAmount').val());

        if (!isNaN(price) && !isNaN(amount)) {
            var total = price * amount;
            $('#limitTotal').val(total.toFixed(2));
        } else {
            $('#limitTotal').val('');
        }
    }

    // Miktar veya Fiyat de�i�ti�inde Toplam� g�ncelle
    $('#limitPrice, #limitAmount').on('input', function () {
        updateTotal();
    });


    // Sat�ra t�klama i�levselli�i
    $('#eventDataTable tbody').on('click', 'tr', function () {
        var data = dataTable.row(this).data();
        currentModalTicker = data[0]; // �u anki modal ticker'� g�ncelle
        updateModalData(currentTickers[currentModalTicker]);
        $('#dataModal').modal('show');
        $('#market-tab').tab('show');
    });

    $('#pairSelect').on('change', function () {
        var selectedPair = $(this).val();
        dataTable.column(0).search(selectedPair ? selectedPair + '$' : '', true, false).draw();
    });

    socket.onerror = function (error) {
        console.log('WebSocket Error: ' + error);
    };


});




function postBuyMarket(symbol, count, price) {
    var postData = {
        Symbol: symbol,
        Count: count,
        Price: price
    };

    $.ajax({
        url: '/Market/BuyMarket',
        method: 'POST',
        data: postData,
        success: function (response) {
            if (response.IsSuccess) {
                alert("Ba�ar�l�: " + response.Message);
                Swal.fire({
                    title: 'Ba�ar�l�!',
                    text: response.Message,
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                });
            } else {
                alert("Hata: " + response.message);
                Swal.fire({
                    title: 'Hata!',
                    text: response.message,
                    icon: 'error',
                    confirmButtonText: 'Tamam'
                });
            }
        }
    });


}


function postSellMarket(symbol, count, price) {
    var postData = {
        Symbol: symbol,
        Count: count,
        Price: price
    };


    $.ajax({
        url: '/Market/SellMarket',
        method: 'POST',
        data: postData,
        success: function (response) {
            if (response.IsSuccess) {
                alert("Ba�ar�l�: " + response.Message);
                Swal.fire({
                    title: 'Ba�ar�l�!',
                    text: response.Message,
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                });
            } else {
                alert("Hata: " + response.message);
                Swal.fire({
                    title: 'Hata!',
                    text: response.message,
                    icon: 'error',
                    confirmButtonText: 'Tamam'
                });
            }
        }

    });

}


function postBuyLimit(symbol, limitAmount, limitPrice) {
    var postData = {
        Symbol: symbol,
        Count: limitAmount,
        Price: limitPrice
    };


    $.ajax({
        url: '/Market/BuyLimit',
        method: 'POST',
        data: postData,
        success: function (response) {
            if (response.IsSuccess) {
                alert("Ba�ar�l�: " + response.Message);
                Swal.fire({
                    title: 'Ba�ar�l�!',
                    text: response.Message,
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                });
            } else {
                alert("Hata: " + response.message);
                Swal.fire({
                    title: 'Hata!',
                    text: response.message,
                    icon: 'error',
                    confirmButtonText: 'Tamam'
                });
            }
        }

    });

}




