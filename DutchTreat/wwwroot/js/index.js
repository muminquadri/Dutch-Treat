﻿var x = 0;
var s = "";
console.log("Hello There!!");
var theForm = $("#theForm");
theForm.hide();
//var theForm = document.getElementById("theForm");
//theForm.hidden = true;
var button = document.getElementById("buyButton");
button.addEventListener("click", function () {
    console.log("Buying Item");
});
var productInfo = document.getElementsByClassName("product-props");
var listItems = productInfo.item[0].children;