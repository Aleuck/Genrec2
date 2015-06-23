$(document).ready(function() {

	c = $.cookie('UFC_cores');
	if (c && c === "contraste") {
		$('body').addClass('ntid-contraste');
		$('#ntid-logo .img-responsive').attr("src", "img/logo-ext-mono.png");
	};

	$("#ntid-btn-contraste").click(function() {
		event.preventDefault();
		$('body').toggleClass('ntid-contraste');
		if ($('body').hasClass('ntid-contraste')) {
			$.cookie("UFC_cores","contraste", {expires: 120, path: '/', domain: '200.18.67.36', secure: false } );
			$('#ntid-logo .img-responsive').attr("src", "img/logo-ext-mono.png");
		} else {
			$.removeCookie('UFC_cores', { path: '/' });
			$('#ntid-logo .img-responsive').attr("src", "img/logo-ext.png");
		};
	});
	
	// PULAR PERGUNTA EM REITORA RESPONDE
	$("a.ntid-pular").click(function() {
		event.preventDefault();
		//
		// AJAX
		//
		$(this).closest('.ntid-box').addClass('ntid-pular').animate({
				height: "toggle"
			}, 400, function() {
				// Animation complete.
		});
	});

	
	
	// VOTAR EM PERGUNTA EM REITORA RESPONDE
	$("a.ntid-votar").click(function() {
		event.preventDefault();
		//
		// AJAX
		//
		$(this).closest('.ntid-box').addClass('ntid-votar').animate({
				height: "toggle"
			}, 400, function() {
				// Animation complete.
		});
	});
	
	
	$('.ntid-box .artigo').readmore({
		moreLink: '<a href="#">Continue lendo</a>',
		lessLink: '<a href="#">Fechar</a>',
		afterToggle: function(trigger, element, expanded) {
			if(!expanded) {
				  $('html, body').animate( { scrollTop: element.parent().offset().top }, {duration: 100 } );
			}
		}
	});

	
});
