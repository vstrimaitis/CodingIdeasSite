function submitPost()
{
    var title = document.getElementById('titleBox').value;
    var content = document.getElementById('contentArea').value;
    var newPost = {
        Title: title,
        Content: content,
        AuthorId: localStorage.getItem("UserId"),
        PublishDate: new Date().toISOString()
    };
    
    
    $.ajax({
        url: URL + 'Post',
        type: 'POST',
        data: newPost,
        success: function(result){
            alert("Post added successfully!");
            window.location = 'index.html';
        },
        error: function(exception){
            alert('Exception: '+exception);
        }
    });
}