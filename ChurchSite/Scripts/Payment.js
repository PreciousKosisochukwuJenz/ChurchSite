$(function () {
        $("#generateInvoice").click(function () {
            if (!$('#Name').val().trim()) {
                $('#Name').css("border-color", "red");
                e.preventDefault();
                $('#Name').focus();
                focusSet = true;
            }
            else {
                $('#Name').css("border-color", "#bdbcbc");
            }
            if (!$('#Email').val().trim()) {
                $('#Email').css("border-color", "red");
                e.preventDefault();
                $('#Email').focus();
                focusSet = true;
            }
            else {
                $('#Email').css("border-color", "#bdbcbc");
            }
            if (!$('#Amount').val().trim() || !$('#Amount').val().trim() == "₦0.00") {
                $('#Amount').css("border-color", "red");
                e.preventDefault();
                $('#Amount').focus();
                focusSet = true;
            }
            else {
                $('#Amount').css("border-color", "#bdbcbc");
            }
            if (!$('#Purpose').val().trim()) {
                $('#Purpose').css("border-color", "red");
                e.preventDefault();
                $('#Purpose').focus();
                focusSet = true;
            }
            else {
                $('#Purpose').css("border-color", "#bdbcbc");
            }
            if ($('#Name').val() != "" || $('#Email').val() != "" || $('#Amount').val() != "" || !$('#Amount').val().trim() == "₦0.00" || $('#Purpose').val() != "") {

                $("#generateInvoice").html("Processing...")
                var data = {
                    Amount: $("#Amount").val(),
                    Name: $("#Name").val(),
                    Email: $("#Email").val(),
                    Reason: $("#Purpose").val(),
                }
                $.ajax({
                    url: "ProcessPayment",
                    data: JSON.stringify(data),
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    success: function (data) {
                        if (data != null) {
                            if (data.Message != "Unsuccessful. Try Later") {
                              
                                $("#generateInvoice").html("Donate");
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
                        $("#generateInvoice").html("Donate");
                    }
                })
            }
        });
    
});