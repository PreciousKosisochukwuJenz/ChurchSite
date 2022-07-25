$(function () {
    $("#generateInvoice").click(function () {
        $("#generateInvoice").html("Processing...")
        var data = {
            Id : 1,
            AmountStr: $("#AmountStr").val(),
            Name: $("#Name").val(),
            command: "mass booking",
        }
        $.ajax({
            url: "/Application/ProcessPayment",
            type: "post",
            dataType: "json",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8;",
            success: function (data) {
                if (data != null) {
                    if (data.Message != "Unsuccessful. Try Later") {
                        $("#generateInvoice").html("Generate invoice");
                    }
                    else {
                        alert("REQUEST SUCCESSFUL")
                    }
                    if (data.Message != "Unsuccessful.Try Later") {
                        $("#InvoiceDiv").show(300);
                        $("#InvoiceReference").text(data.InvoiceReference);
                        $("#AccountNumber").text(data.AccountNumber);
                        $("#AccountName").text(data.AccountName);
                        $("#BankCode").text(data.BankCode);
                        $("#BankName").text(data.BankName);
                        $("#StudentName").text(data.CustomerName);
                        $("#StudentEmail").text(data.CustomerEmail);
                        $("#Description").text(data.Description);
                        $("#ExpiryDate").text(data.ExpiryDate);
                        $("#generateInvoice").hide();

                        var mydiv = document.getElementById("link");
                        var aTag = document.createElement('a');
                        aTag.setAttribute('href', data.CheckoutUrl);
                        aTag.textContent = "Click to pay online.";
                        mydiv.appendChild(aTag);
                        var printbtn = document.getElementById("printBtn");
                        printbtn.setAttribute('href', "MonnifyPaymentInvoice?storageID=" + data.storageID);

                    }
                }
            },
            error: function (ex) {
                toastr.error('Failed to save Data.' + ex, 'Error!', { timeOut: 5000 });
                $("#generateInvoice").html("Generate invoice");
            }
        })
	});
});