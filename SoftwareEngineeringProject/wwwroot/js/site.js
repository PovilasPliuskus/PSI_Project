// Add Note button click
document.getElementById("addNoteButton").addEventListener("click", function () {
    fetch("Home/CreateNote")
        .then(response => response.json())
        .then(data => {
            const textarea = document.createElement("textarea");

            textarea.id = data.id;
            textarea.name = data.name;
            textarea.value = data.value;
            textarea.rows = data.rows;
            textarea.cols = data.columns;
            textarea.category = data.category

            textarea.classList.add("note-textarea");
            document.getElementById("noteContainer").appendChild(textarea);
        })
        .catch(error => {
            console.error("Error:", error);
        });
});
// Save note button click
document.getElementById("saveNoteButton").addEventListener("click",function(){
    const textarea = document.querySelectorAll(".note-textarea");
    const notesData = [];
    textarea.forEach(textarea => {
        notesData.push({
            Id: textarea.id,
            Name: textarea.name,
            Value: textarea.value,
            Rows: textarea.rows,
            Columns: textarea.cols,
            Category: textarea.category
        });
    });
    // Send the notesData to the server for saving
    fetch("Home/SaveNote", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(notesData)
    })
});