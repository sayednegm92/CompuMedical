// Handle tab click
$('.tab-item').click(function () {
    // Remove active class from all tabs
    $('.tab-item').removeClass('active');

    // Add active class to clicked tab
    $(this).addClass('active');

    // Get target tab content
    const target = $(this).data('target');

    // Hide all tab content
    $('.tab-pane').removeClass('active');

    // Show target tab content
    $('#' + target).addClass('active');

    // Move the indicator
    moveIndicator($(this));
});

// Function to move the indicator
function moveIndicator(element) {
    const tabWidth = $('.tabs').width() / $('.tab-item').length;
    const index = $('.tab-item').index(element);
    const position = index * tabWidth;

    $('.tab-indicator').css({
        'width': tabWidth + 'px',
        'transform': 'translateX(' + position + 'px)'
    });
}

// Initialize the indicator position
moveIndicator($('.tab-item.active'));

// Handle window resize
$(window).resize(function () {
    moveIndicator($('.tab-item.active'));
});