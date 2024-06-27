function ChangeFollowButton(Value) {
    var button = document.getElementById("Follow-Button");
    if (button) {
        button.innerText = Value;
    }
}
function GenerateBIOForm(BIOContent, UserId) {
    var BIOContainer = document.getElementById("UserBio-Conatiner");
    var FormContainer = document.createElement("div");
    BIOContainer.appendChild(FormContainer);
    var ChangeBioButton = document.getElementById("ChangeBioButtonIcon");
    ChangeBioButton.hidden = true;
    var BioParagarph = document.getElementById("UserBio");
    BioParagarph.hidden = true;
    var InputBio = document.createElement("textarea");
    InputBio.type = "text";
    InputBio.value = BIOContent;
    InputBio.id = "InputBio";
    InputBio.maxLength = 200;
    var Button = document.createElement("button");
    Button.id = "ChangeBioButton";
    Button.onclick = function () {
        var BioInput = document.getElementById("textarea");
        if (BioInput != null) {
            var BioContent = BioInput.value;
            var BioParagarph = document.getElementById("UserBio");
            BioParagarph.textContent = BioContent;
            fetch(`/Bio/ChangeBio?Bio=${BioContent}&UserId=${UserId}`, {
                method: "POST"
            });
            RemoveInupt();
        }
    }
    Button.textContent = "Change Bio";
    FormContainer.append(InputBio);
    FormContainer.append(Button);
}
function RemoveInuptBio() {
    var InputBio = document.getElementById("InputBio");
    InputBio.remove();
    var BioParagarph = document.getElementById("UserBio");
    BioParagarph.hidden = false;
    var ChangeBioButtonIcon = document.getElementById("ChangeBioButtonIcon");
    ChangeBioButtonIcon.hidden = false;
    var ChangeBioButton = document.getElementById("ChangeBioButton");
    ChangeBioButton.remove();
}
function RemovePost(PostId) {
    var Post = document.getElementById(`Post-${PostId}`);
    Post.remove();
}
function GenerateEditPostForm(Content, PostId) {
    var Paragraph = document.getElementById(`PostContent-${PostId}`);
    var ParagraphParent = Paragraph.parentNode;
    Paragraph.hidden = true;
    var RemoveButton = document.getElementById(`RemoveButton-${PostId}`);
    RemoveButton.hidden = true;
    var EditButton = document.getElementById(`EditButton-${PostId}`);
    EditButton.hidden = true;
    var Input = document.createElement("Input");
    ParagraphParent.appendChild(Input);
    Input.value = Content;
    Input.type = "text";
    Input.id = "InputEdit";
    Input.maxLength = 200;
    var Button = document.createElement("button");
    var ButtonParent = document.getElementById(`CreatorContainer-${PostId}`);
    ButtonParent.appendChild(Button);
    Button.id = "EditContent-Button";
    Button.textContent = "Change content";
    Button.onclick = function () {
        var Input = document.getElementById("InputEdit");
        if (Input != null) {
            var Content = Input.value;
            var Paragarph = document.getElementById(`PostContent-${PostId}`);
            Paragarph.textContent = Content;
            fetch(`/Post/ChangeContent?Content=${Content}&PostId=${PostId}`, {
                method: "POST"
            });
            RemoveInuptContent(PostId);
        }
    }
}
function RemoveInuptContent(PostId) {
    var Input = document.getElementById("InputEdit");
    Input.remove();
    var Paragraph = document.getElementById(`PostContent-${PostId}`);
    Paragraph.hidden = false;
    var RemoveButton = document.getElementById(`RemoveButton-${PostId}`);
    RemoveButton.hidden = false;
    var EditButton = document.getElementById(`EditButton-${PostId}`);
    EditButton.hidden = false;
    var ChangeContnetButton = document.getElementById("EditContent-Button");
    ChangeContnetButton.remove();
}
