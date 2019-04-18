//(function() {
$(document).ready(function () {
    var x = 0;
    var s = "";
    console.log("Hello There!!");
    var theForm = $("#theForm");
    theForm.hide();
    //var theForm = document.getElementById("theForm");
    //theForm.hidden = true;
    //var button = document.getElementById("buyButton");
    //button.addEventListener("click", function () {
    //    console.log("Buying Item");
    //});
    var button = $("#buyButton");
    button.on("click", function () {
        console.log("Buying Item");
    });
    //var productInfo = document.getElementsByClassName("product-props");
    //var listItems = productInfo.item[0].children;
    var productInfo = $(".product-props li");
    productInfo.on("click", function () {
        //console.log("You clicked on" + this.innerText);
        console.log("You clicked on " + $(this).text());
    });
    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");
    $loginToggle.on("click", function () {
        $popupForm.toggle(1000);
        console.log("hey");
        //$popupForm.fadeToggle(1200);
       // $popupForm.slideToggle(1000);
    })
 //Validation for popup form
    const passwordRegex = /^[0-9a-zA-z]{10}$/
    var password = document.querySelector(".pwd");
    var errorMessage = document.querySelector("#error");
    //Add Event Listener for Validation---Event Handling
    password.addEventListener("keyup", function (e) {
        if (passwordRegex.test(e.target.value)) {            
            password.style.border = "2px solid green";
            errorMessage.style.display = "none";
            
        }
        else {
            password.style.border = "2px solid red";
            errorMessage.style.display = "block";             
        }
    })
    const userNameRegex = /^[A-Z0-9a-z._]+@[A-Za-z]+\.+[a-z]{2,4}$/
    var userName = document.querySelector(".userName");
    var userNameErrorMessage = document.querySelector("#errorMessage");
    userName.addEventListener("keyup", function (e) {
        if (userNameRegex.test(e.target.value)) {
            userName.style.border = "2px solid green";
            userNameErrorMessage.style.display = "none";
            console.log("tsest");
        }
        else {
            console.log("tsnjjnest");
            userName.style.border = "2px solid red";
            userNameErrorMessage.style.display = "block";
        }
    })
    //Validation logic ends here

    //});
});