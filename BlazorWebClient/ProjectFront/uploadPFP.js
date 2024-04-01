document.getElementById('accountPicture').addEventListener('click', function() {
    document.getElementById('accountPictureInput').click();
});

document.getElementById('accountPictureInput').addEventListener('change', function(event) {
    var input = event.target;
    var reader = new FileReader();

    reader.onload = function() {
        document.getElementById('accountPicture').src = reader.result;
    };

    reader.readAsDataURL(input.files[0]);
});