function AddCommentBlock(PostId) {
    var PostDiv = document.getElementById("Post-" + PostId);
    var NumberOfChilds = PostDiv.childElementCount;
    if (NumberOfChilds == 3)
    {
        var newDiv = document.createElement('div');
        newDiv.classList.add('Comment-Block');
        newDiv.id = "CommentBlock-" + PostId;
        PostDiv.appendChild(newDiv);

        var CloseButton = document.createElement("button");
        CloseButton.onclick = function () { ShoutCommentBlock("CommentBlock-" + PostId); };
        CloseButton.className = "CloseButton";
        CloseButton.id = "CloseButton-" + PostId;
        newDiv.appendChild(CloseButton);

        var CloseIcon = document.createElement("img");
        CloseIcon.className = "CloseIcon";
        CloseIcon.src = "/Icons/CloseIcon.png";
        CloseButton.appendChild(CloseIcon);
    }
   
}
function ShoutCommentBlock(ElementId) {
    var CommentBlock = document.getElementById(ElementId);
    CommentBlock.remove();
}
function AddComment(CommentBlockId, CreatorName, Content) {
    var CommentBlock = document.getElementById(CommentBlockId);
    var CommentDiv = document.createElement("div");
    var CreatorNameDiv = document.createElement("div");
    var ContentDiv = document.createElement("div");

    CommentDiv.className = "Comment";
    CreatorNameDiv.className = "Comment-CreatorName";
    ContentDiv.className = "Comment-Content";

    CreatorNameDiv.textContent = CreatorName;
    ContentDiv.textContent = Content;

    CommentDiv.appendChild(CreatorNameDiv);
    CommentDiv.appendChild(ContentDiv);
    CommentBlock.appendChild(CommentDiv);
}