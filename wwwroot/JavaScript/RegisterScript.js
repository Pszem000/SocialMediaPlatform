document.addEventListener('DOMContentLoaded', (event) => {
    var FileInput = document.getElementById("CustomFiles");
    FileInput.addEventListener('change', function () {
        var fileName = FileInput.files[0];
        var label = document.getElementById("CustomFile-Label");
        label.innerText = "File is seleced";
    });
});