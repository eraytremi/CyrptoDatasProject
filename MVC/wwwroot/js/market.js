
$(document).ready(function () {

    var currentTickers = {};
    var currentModalTicker = null;

    // WebSocket baðlantýsý
    const socket = new WebSocket('wss://stream.binance.com:9443/ws/!ticker@arr');
    var tickers;
    socket.onmessage = function (event) {
        tickers = JSON.parse(event.data);
        updateTableData(tickers);



        tickers.forEach(function (ticker) {
            // Sadece belirli hedef para birimleri için iþle
            var targetCurrency = ticker.s.substr(-4); // Paritenin son 4 karakterini al
            var isTargetCurrency = ['USDT'].includes(targetCurrency);

           

            if (isTargetCurrency) {
                // Bu kýsým yukarýdaki kodla ayný
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

                // Eðer modal açýksa ve güncellenen ticker, modalda gösterilen ticker ise
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
                    ticker.p, // Fiyat Deðiþimi
                    ticker.P, // Fiyat Deðiþimi (%)
                    ticker.c  // Fiyat
                ];

                // Yeni verilerle satýrý güncelle
                existingRow.data(newData).draw(false);

                // Her hücre için renk güncellemesi yap
                updateTableRowColor(existingRow, oldData, newData);
            }
        });
    }



    // Modal içeriðini güncelle
    function updateModalData(ticker) {
        $('#modalSymbol').text(ticker.s);
        $('#modalLastPrice').text(ticker.c);
    }

    // Miktar giriþi için event handler
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
    //Limit Emir sekmesine geçildiðinde ve modal açýldýðýnda fiyatý güncelle
    function updateLimitPrice() {
        var currentPrice = $('#modalLastPrice').text();
        $('#limitPrice').val(currentPrice);
    }

    // Limit Emir sekmesine geçiþ yapýldýðýnda fiyatý güncelle
    $('#limit-tab').on('shown.bs.tab', function (e) {
        updateLimitPrice();
    });

    // Modal açýldýðýnda fiyatý güncelle
    $('#dataModal').on('shown.bs.modal', function (e) {
        if ($('#limit-tab').hasClass('active')) {
            updateLimitPrice();
        }
    });


    // Limit emir Toplamý hesapla ve göster
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

    // Miktar veya Fiyat deðiþtiðinde Toplamý güncelle
    $('#limitPrice, #limitAmount').on('input', function () {
        updateTotal();
    });


    // Satýra týklama iþlevselliði
    $('#eventDataTable tbody').on('click', 'tr', function () {
        var data = dataTable.row(this).data();
        currentModalTicker = data[0]; // Þu anki modal ticker'ý güncelle
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
                alert("Baþarýlý: " + response.Message);
                Swal.fire({
                    title: 'Baþarýlý!',
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
                alert("Baþarýlý: " + response.Message);
                Swal.fire({
                    title: 'Baþarýlý!',
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
                alert("Baþarýlý: " + response.Message);
                Swal.fire({
                    title: 'Baþarýlý!',
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




