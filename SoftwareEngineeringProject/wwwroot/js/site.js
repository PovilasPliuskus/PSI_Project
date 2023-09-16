// Add Note button click
document.getElementById("addNoteButton").addEventListener("click", function () {
    fetch("Home/CreateNote")
        .then(response => response.json())
        .then(data => {
            document.getElementById("noteInfo").innerHTML = `
            <p>Name: ${data.name}</p>
            <p>Value: ${data.value}</p>
            <p>Rows: ${data.rows}</p>
            <p>Columns: ${data.columns}</p>
            <p>Creation Date: ${data.creationDate}</p>
            <p>ID: ${data.id}</p>
            <p>Category: ${data.category}</p>
            `;
        })
        .catch(error => {
            console.error("Error:", error);
        });
});