window.addEventListener('load', () => {
    
    if (!sessionStorage.getItem('alertShown')) {
        setTimeout(() => {
            alert('Welcome to Braz\'s Tours! Explore the beauty of Ireland.');
            
            sessionStorage.setItem('alertShown', 'true');
        }, 1000);
    }
});

$(document).ready(function () {

    const currentDateElement = document.getElementById('current-date');
    const today = new Date();
    const formatteDate = today.toLocaleDateString('en-IE', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' });
    currentDateElement.textContent = formatteDate;

    $("#wicklow").click(function () { $(this).css({ "transform": "rotate(360deg)", "transition": "transform 1s ease" }); });
    $("#cliffs").click(function () { $(this).hide(500).show(500); });
    $("#giants").click(function () { $(this).slideUp(500).slideDown(500); });
    $("#malahide").click(function () { $(this).fadeOut(500).fadeIn(500); });
    $("#newgrange").click(function () { $(this).animate({ marginLeft: "50px" }, 500).animate({ marginLeft: "0px" }, 500); });
});