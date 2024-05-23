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