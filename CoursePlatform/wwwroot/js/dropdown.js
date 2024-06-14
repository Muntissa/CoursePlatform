

/* Please ❤ this if you like it! */


(function($) { "use strict";

	$(function() {
		var header = $(".start-style");
		$(window).scroll(function() {    
			var scroll = $(window).scrollTop();
		
			if (scroll >= 10) {
				header.removeClass('start-style').addClass("scroll-on");
			} else {
				header.removeClass("scroll-on").addClass('start-style');
			}
		});
	});		
		
	//Animation
	
	$(document).ready(function() {
		$('body.hero-anime').removeClass('hero-anime');
	});

	//Menu On Hover
		
	$('body').on('mouseenter mouseleave','.nav-item',function(e){
			if ($(window).width() > 750) {
				var _d=$(e.target).closest('.nav-item');_d.addClass('show');
				setTimeout(function(){
				_d[_d.is(':hover')?'addClass':'removeClass']('show');
				},1);
			}
	});	
	
	//Switch light/dark
	
	$("#switch").on('click', function () {
		if ($("body").hasClass("dark")) {
			$("body").removeClass("dark");
			$("#switch").removeClass("switched");
		}
		else {
			$("body").addClass("dark");
			$("#switch").addClass("switched");
		}
	});  
	
})(jQuery); 

// Автоматический размер textarea
function autoResizeTextarea(textarea) {
	// Сбрасываем высоту
	textarea.style.height = 'auto';
	// Устанавливаем высоту по содержимому
	textarea.style.height = textarea.scrollHeight + 'px';
}

document.addEventListener('DOMContentLoaded', function() {
	const textarea = document.getElementById('autoResizeTextarea');

	// Изначально изменяем высоту
	autoResizeTextarea(textarea);

	// Изменяем высоту при вводе
	textarea.addEventListener('input', function() {
		autoResizeTextarea(textarea);
	});

	// Изменяем высоту при загрузке содержимого, если есть предварительное значение
	textarea.addEventListener('change', function() {
		autoResizeTextarea(textarea);
	});
});