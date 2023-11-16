window.addEventListener("load",function (){
    loadNotesFromFile();
});

function loadNotesFromFile(){
    fetch("/Notes/LoadNotes")
        .then(response => response.json())
        .then(data => {
            noteList.push(...data);
            noteList.forEach(function (note) {

                const button = createButton(note);
                const textareaContainer = document.getElementById("opened-notes-div");
                replaceTextHeader(textareaContainer, note);
                replaceTextarea(textareaContainer, note);

                document.getElementById("listOfNotes").appendChild(button);
            });
        })
        .catch(error => {
            console.error("Error loading notes to server:", error);
        });
}