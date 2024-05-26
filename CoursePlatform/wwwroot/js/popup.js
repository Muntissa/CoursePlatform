$('.popup-reg').click(function(e) {
    e.preventDefault();
    $('.popup-reg-bg').fadeIn(400);
    $('html').addClass('no-scroll');
});

$('.popup-reg-bg').click(function(e) {
    // Проверяем, был ли клик по самой фоновой области
    if ($(e.target).hasClass('popup-reg-bg')) {
        $(this).fadeOut(200);
        $('html').removeClass('no-scroll');
    }
});

$('.popup-auth').click(function(e) {
    e.preventDefault();
    $('.popup-auth-bg').fadeIn(400);
    $('html').addClass('no-scroll');
});

$('.popup-auth-bg').click(function(e) {
    // Проверяем, был ли клик по самой фоновой области
    if ($(e.target).hasClass('popup-auth-bg')) {
        $(this).fadeOut(200);
        $('html').removeClass('no-scroll');
    }
});