"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

// $(function () {
//     connection.start().then(function () {
//         alert("Conected to dashboard hub.");
//     }).catch(function (err) {
//         return console.error(err.toString());
//     });
// });

connection.start().then(function () {
    //alert("Conected to dashboard hub.");

    InvokeProducts();
}).catch(function (err) {
    return console.error(err.toString());
});


// Product
function InvokeProducts() {
	connection.invoke("SendProducts").catch(function (err) {
		return console.error(err.toString());
	});
}

connection.on("ReceivedProducts", function (products) {
	BindProductsToGrid(products);
});

function BindProductsToGrid(products) {
    console.log(JSON.stringify(products[0]))
	$('#tblProduct tbody').empty();  

	var tr;
	$.each(products, function (index, product) {
        console.log(JSON.stringify(product))
		tr = $('<tr/>');
		tr.append(`<td>${(product.newsID)}</td>`);
		tr.append(`<td>${product.newsTitle}</td>`);
		tr.append(`<td>${product.providerID}</td>`);
		$('#tblProduct').append(tr);
	});
}