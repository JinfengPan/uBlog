import '../scss/prostyles.scss';

$(document).ready(function(){
    $(".nav-button").click(function(){
        $('.nav-button,.primary-nav').toggleClass("open");
    });
});