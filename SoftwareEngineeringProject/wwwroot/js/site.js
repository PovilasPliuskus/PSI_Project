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
    fetch("/ProjectPages/SaveNote", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(notesData)
    })
});