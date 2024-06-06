document.addEventListener("DOMContentLoaded", function () {
    const textButton = document.getElementById('textButton');
    const imageButton = document.getElementById('imageButton');
    const videoButton = document.getElementById('videoButton');
    const fileButton = document.getElementById('fileButton');
    const testButton = document.getElementById('testButton');
    const saveContentButton = document.getElementById('saveContentButton');

    const textBlock = document.getElementById('textBlock');
    const imageBlock = document.getElementById('imageBlock');
    const videoBlock = document.getElementById('videoBlock');
    const fileBlock = document.getElementById('fileBlock');
    const testBlock = document.getElementById('testBlock');

    const blocks = [textBlock, imageBlock, videoBlock, fileBlock, testBlock];

    function hideAllBlocks() {
        blocks.forEach(block => block.style.display = 'none');
    }

    function showBlock(block) {
        hideAllBlocks();
        if (block === videoBlock) {
            block.style.display = 'flex';
        } else {
            block.style.display = 'block';
        }
    }

    textButton.addEventListener('click', () => showBlock(textBlock));
    imageButton.addEventListener('click', () => showBlock(imageBlock));
    videoButton.addEventListener('click', () => showBlock(videoBlock));
    fileButton.addEventListener('click', () => showBlock(fileBlock));
    testButton.addEventListener('click', () => showBlock(testBlock));

    saveContentButton.addEventListener('click', function () {
        let activeForm;
        blocks.forEach(block => {
            if (block.style.display === 'block' || block.style.display === 'flex') {
                activeForm = block.querySelector('form');
            }
        });

        if (activeForm) {
            activeForm.submit();
        }
    });

    // Hide all blocks initially
    hideAllBlocks();

    // Open the appropriate block if it has content
    if (textBlock.querySelector('textarea').value) showBlock(textBlock);
    if (videoBlock.querySelector('input').value) showBlock(videoBlock);
    if (imageBlock.querySelector('input').files.length > 0) showBlock(imageBlock);
    if (fileBlock.querySelector('input').files.length > 0) showBlock(fileBlock);
});
