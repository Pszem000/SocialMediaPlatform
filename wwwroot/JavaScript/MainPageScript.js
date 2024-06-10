function AddCommentBlock(PostId) {
    var PostDiv = document.getElementById("Post-" + PostId);
    var NumberOfChilds = PostDiv.childElementCount;
    if (NumberOfChilds == 3)
    {
        var newDiv = document.createElement('div');
        newDiv.classList.add('Comment-Block');
        newDiv.id = "CommentBlock-" + PostId;
        PostDiv.appendChild(newDiv); 
        
        var CloseButtonContainer = document.createElement("div");
        CloseButtonContainer.className = "CloseButton-Container";
        
        var CloseButton = document.createElement("button");
        CloseButton.onclick = function () { ShoutCommentBlock("CommentBlock-" + PostId); };
        CloseButton.className = "CloseButton";
        CloseButton.id = "CloseButton-" + PostId;
        CloseButton.style.backgroundImage = "/Icons/CloseIcon.png";
        newDiv.appendChild(CloseButtonContainer);

        var CloseIcon = document.createElement("img");
        CloseIcon.className = "CloseIcon";
        CloseIcon.src = "/Icons/CloseIcon.png";
        CloseButton.appendChild(CloseIcon);
        CloseButtonContainer.appendChild(CloseButton);

        var AddCommentDiv = document.createElement("div");
        AddCommentDiv.className = "AddComment-Container";
        var AddCommentInputContainer = document.createElement("div");
        AddCommentInputContainer.className = "AddComment-InputContainer";
        var AddCommentButtonContainer = document.createElement("div");
        AddCommentButtonContainer.className = "AddComment-ButtonContainer";
        newDiv.appendChild(AddCommentDiv);
        AddCommentDiv.appendChild(AddCommentInputContainer);
        AddCommentDiv.appendChild(AddCommentButtonContainer);
    
        var AddCommentInput = document.createElement("input");
        AddCommentInput.className = "AddComment-Input";
        AddCommentInput.type = "text";
        AddCommentInput.id = "AddComment-Input-" + PostId;
        AddCommentInput.placeholder = "Enter Content";
        AddCommentInputContainer.appendChild(AddCommentInput);

        var AddCommentButton = document.createElement("button");
        AddCommentButton.className = "AddCommentButton";
        AddCommentButton.textContent = "Add";
        AddCommentButton.onclick = function () {
            var Content = document.getElementById("AddComment-Input-" + PostId).value;
            fetch(`/Comments/AddComment?Content=${Content }&PostId=${PostId}`);
            document.getElementById("AddComment-Input-" + PostId).value = ""; 
        }
        AddCommentButtonContainer.appendChild(AddCommentButton);

        var CommentsContainer = document.createElement("div");
        CommentsContainer.className = "CommentContainer";
        CommentsContainer.id = "CommentContainer-" + PostId;
        newDiv.appendChild(CommentsContainer);
    }
   
}
function ShoutCommentBlock(ElementId) {
    var CommentBlock = document.getElementById(ElementId);
    CommentBlock.remove();
}
function AddComment(PostId, CreatorName, Content) {
    var CommentBlock = document.getElementById("CommentContainer-" + PostId);
    var CommentDiv = document.createElement("div");
    var CreatorNameDiv = document.createElement("div");
    var ContentDiv = document.createElement("div");

    CommentDiv.className = "Comment";
    CreatorNameDiv.className = "Comment-CreatorName";
    ContentDiv.className = "Comment-Content";

    CreatorNameDiv.textContent = CreatorName;
    ContentDiv.innerHTML = Content;

    CommentDiv.appendChild(CreatorNameDiv);
    CommentDiv.appendChild(ContentDiv);
    CommentBlock.appendChild(CommentDiv);
}

function ChangePhoto(ElementId, NewSrc) {
    var photo = document.getElementById(ElementId);
    photo.src = NewSrc;
}