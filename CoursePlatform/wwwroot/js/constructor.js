document.addEventListener("DOMContentLoaded", function () {
    const textButton = document.getElementById('textButton');
    const imageButton = document.getElementById('imageButton');
    const videoButton = document.getElementById('videoButton');
    const fileButton = document.getElementById('fileButton');
    const saveContentButton = document.getElementById('saveContentButton');

    const textBlock = document.getElementById('textBlock');
    const imageBlock = document.getElementById('imageBlock');
    const videoBlock = document.getElementById('videoBlock');
    const fileBlock = document.getElementById('fileBlock');

    const blocks = [textBlock, imageBlock, videoBlock, fileBlock];

    function hideAllBlocks() {
        blocks.forEach(block => block.style.display = 'none');
    }

    function showBlock(block) {
        hideAllBlocks();
        block.style.display = 'block';
    }

    textButton.addEventListener('click', () => showBlock(textBlock));
    imageButton.addEventListener('click', () => showBlock(imageBlock));
    videoButton.addEventListener('click', () => showBlock(videoBlock));
    fileButton.addEventListener('click', () => showBlock(fileBlock));

    saveContentButton.addEventListener('click', function () {
        let activeForm;
        blocks.forEach(block => {
            if (block.style.display === 'block') {
                activeForm = block.querySelector('form');
            }
        });

        if (activeForm) {
            activeForm.submit();
        }
    });

    // Open the appropriate block if it has content
    if (textBlock.querySelector('textarea').value) showBlock(textBlock);
    if (videoBlock.querySelector('input').value) showBlock(videoBlock);
    if (imageBlock.querySelector('input').files.length > 0) showBlock(imageBlock);
    if (fileBlock.querySelector('input').files.length > 0) showBlock(fileBlock);
});
