function submitFormAddBill(event) {
    event.preventDefault();

    console.log(event);
    billName = document.getElementsByName('Name')[0].value;
    billIssuedDate = document.getElementsByName('IssuedDate')[0].value;
    billDueDate = document.getElementsByName('DueDate')[0].value;
    billIssuedBy = document.getElementsByName('IssuedBy')[0].value;
    billAmount = document.getElementsByName('Amount')[0].value;
    billIsPaid = document.getElementsByName('IsPaid')[0].checked;

    xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://localhost:4000/bills/create');
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.onload = function() {
        if (xhr.status === 200) {
            console.log('Added');
            console.log(JSON.parse(xhr.responseText))
        } else if (xhr.status !== 200) {
            console.log('Error');
            console.log(JSON.parse(xhr.responseText))
        }
    };
    xhr.send(JSON.stringify({
        Name: billName,
        IssuedDate: billIssuedDate,
        DueDate: billDueDate,
        IssuedBy: billIssuedBy,
        Amount: parseInt(billAmount),
        IsPaid: billIsPaid
    }));
};


function loadBills() {

    xhr = new XMLHttpRequest();
    xhr.open('GET', 'http://localhost:4000/bills/getAll');
    xhr.onload = function() {
        if (xhr.status === 200) {
            console.log("Get All");

            bills = JSON.parse(xhr.responseText);

            bills.forEach(bill => {

                newTableRow = document.createElement("tr");

                billId = document.createElement("th");
                billId.innerHTML = bill.id;
                billId.setAttribute("hidden", true);

                billName = document.createElement("th");
                billName.innerHTML = bill.name;

                billIssuedDate = document.createElement("th");
                billIssuedDate.innerHTML = bill.issuedDate.substring(0, 10);

                billDueDate = document.createElement("th");
                billDueDate.innerHTML = bill.dueDate.substring(0, 10);

                billIssuedBy = document.createElement("th");
                billIssuedBy.innerHTML = bill.issuedBy;

                billAmount = document.createElement("th");
                billAmount.innerHTML = bill.amount;

                billIsPaid = document.createElement("th");
                if (bill.isPaid == true) {
                    billIsPaid.innerHTML = '<input type="checkbox" disabled checked />'
                } else {
                    billIsPaid.innerHTML = '<input type="checkbox" disabled />'
                }

                actions = document.createElement("th");
                actions.innerHTML = '<button onclick="deleteBill(event);"><i class="fa fa-trash" aria-hidden="true"></i></button> | ' +
                    '<button onclick="changeIsPaidValueBill(event);"><i class="fa fa-check" aria-hidden="true"></i></button>';

                newTableRow.appendChild(billId);
                newTableRow.appendChild(billName);
                newTableRow.appendChild(billIssuedDate);
                newTableRow.appendChild(billDueDate);
                newTableRow.appendChild(billIssuedBy);
                newTableRow.appendChild(billAmount);
                newTableRow.appendChild(billIsPaid);
                newTableRow.appendChild(actions);

                table = document.getElementById("billsTable");
                table.appendChild(newTableRow);

                console.log(bill);
            });
        } else if (xhr.status !== 200) {
            console.log("Error");
            console.log(JSON.parse(xhr.responseText));
        }
    };
    xhr.send();
};


function deleteBill(event) {

    tableRow = event.target.closest('tr');
    billId = tableRow.firstChild.innerHTML;

    xhr = new XMLHttpRequest();
    xhr.open('DELETE', 'http://localhost:4000/bills/delete/' + billId);
    xhr.onload = function() {
        if (xhr.status === 200) {
            console.log("Delete Bill");

            event.target.closest('tr').remove();

        } else if (xhr.status !== 200) {
            console.log("Error");
            console.log(JSON.parse(xhr.responseText));
        }
    };
    xhr.send();
}

function changeIsPaidValueBill(event) {
    tableRow = event.target.closest('tr');
    billId = tableRow.firstChild.innerHTML;

    xhr = new XMLHttpRequest();
    xhr.open('PUT', 'http://localhost:4000/bills/changeIsPaidValue/' + billId);
    xhr.onload = function() {
        if (xhr.status === 200) {
            console.log("Changed Is Paid Value");

            isPaidInput = tableRow.querySelectorAll('input')[0];
            isPaidInput.checked = !isPaidInput.checked;

        } else if (xhr.status !== 200) {
            console.log("Error");
            console.log(JSON.parse(xhr.responseText));
        }
    };
    xhr.send();
}