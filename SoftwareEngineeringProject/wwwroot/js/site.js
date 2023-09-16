// Add Note button click
document.getElementById("addNoteButton").addEventListener("click", function () {
    fetch("Home/CreateNote")
        .then(response => response.json())
        .then(data => {
            const textarea = document.createElement("textarea");

            textarea.value = data.value;
            textarea.cols = data.columns;
            textarea.rows = data.rows;

            textarea.classList.add("note-textarea");

            document.getElementById("noteContainer").appendChild(textarea);
        })
        .catch(error => {
            console.error("Error:", error);
        });
});