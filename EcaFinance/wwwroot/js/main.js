$(window).load(function(){
     $('.preloader').fadeOut('slow');
});

/* =Main INIT Function
-------------------------------------------------------------- */
function initializeSite() {

	"use strict";

	//OUTLINE DIMENSION AND CENTER
	(function() {
	    function centerInit(){

			var sphereContent = $('.sphere'),
				sphereHeight = sphereContent.height(),
				parentHeight = $(window).height(),
				topMargin = (parentHeight - sphereHeight) / 2;

			sphereContent.css({
				"margin-top" : topMargin+"px"
			});
	    }

	    $(document).ready(centerInit);
		$(window).resize(centerInit);
	})();

	// Init effect 
	//$('#scene').parallax();

};
/* END ------------------------------------------------------- */

/* =Document Ready Trigger
-------------------------------------------------------------- */
$(window).load(function(){

	initializeSite();
	(function() {
		setTimeout(function(){window.scrollTo(0,0);},0);
	})();

});
/* END ------------------------------------------------------- */


$('#countdown').countdown({
    date: "May 12 2024 11:00:00",
    render: function (data) {
        var el = $(this.el);
        el.empty()
            //.append("<div>" + this.leadingZeros(data.years, 4) + "<span>years</span></div>")
            //.append("<div>" + this.leadingZeros(data.days, 2) + " <span>days</span></div>")
            .append('<div class="timehours">' + this.leadingZeros(data.hours, 2) + " <span>hrs</span></div>")
            .append('<div class="timemin">' + this.leadingZeros(data.min, 2) + " <span>min</span></div>")
            .append('<div class="timesec">' + this.leadingZeros(data.sec, 2) + " <span>sec</span></div>");
        if (data.hours == 0) {
            $(".timehours").hide();
        }
		if (data.min == 0 && data.hours == 0) {
            $(".timemin").hide()
        }
		if (data.sec == 0 && data.min == 0 && data.hours == 0) {
            $(".timesec").hide()
        }
	}

});


var getNextDay = function (dayName) {

	var date = new Date();
	var now = date.getDay();

	var days = ['sunday', 'monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday'];

	var day = days.indexOf(dayName.toLowerCase());

	var diff = day - now;
	diff = diff < 1 ? 7 + diff : diff;

	var nextDayTimestamp = date.getTime() + (1000 * 60 * 60 * 24 * diff);

	return new Date(nextDayTimestamp);
};

function toggleFullScreen(elem) {
    // ## The below if statement seems to work better ## if ((document.fullScreenElement && document.fullScreenElement !== null) || (document.msfullscreenElement && document.msfullscreenElement !== null) || (!document.mozFullScreen && !document.webkitIsFullScreen)) {
    if ((document.fullScreenElement !== undefined && document.fullScreenElement === null) || (document.msFullscreenElement !== undefined && document.msFullscreenElement === null) || (document.mozFullScreen !== undefined && !document.mozFullScreen) || (document.webkitIsFullScreen !== undefined && !document.webkitIsFullScreen)) {
        if (elem.requestFullScreen) {
            elem.requestFullScreen();
        } else if (elem.mozRequestFullScreen) {
            elem.mozRequestFullScreen();
        } else if (elem.webkitRequestFullScreen) {
            elem.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
        } else if (elem.msRequestFullscreen) {
            elem.msRequestFullscreen();
        }
    } else {
        if (document.cancelFullScreen) {
            document.cancelFullScreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitCancelFullScreen) {
            document.webkitCancelFullScreen();
        } else if (document.msExitFullscreen) {
            document.msExitFullscreen();
        }
    }
}