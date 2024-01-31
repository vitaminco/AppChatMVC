//bổ sung prototype cho kiểu date
Date.prototype.toDMYHMS = function () {
    let dateFormat = this.getDate() + "/" + this.getMonth() + 1 + "/" + this.getFullYear();
    let timeFormat = this.getHours() + ":" + this.getMinutes() + ":" + this.getSeconds();
    return `${dateFormat} ${timeFormat}`;
}


document.addEventListener('alpine:init', () => {
    Alpine.data('chatdata', () => ({
        _me: '',
        _connection: {},
        _friends: [],
        _searchFriend: [],
        _messages: [],

        init() {
            this._me = document.querySelector("#me").value;  //lấy username đang đăng nhập để so sánh
            this._connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();
            this._connection.start();

            let url = "/Friend/GetAll";
            fetch(url)
                .then(res => res.json())
                .then(json => {
                    this._friends = json
                });

            //sự kiện nhận dữ liệu
            this._connection.on("ReceiveMessage", ($sender, $message, $time) => {
                let mesg = {
                    isFromMe: $sender == this._me,
                    message: $message,
                    time: new Date($time).toDMYHMS()
                };
                this._messages.push(mesg);
            });

            this._connection.on("ReceiveAddFriendTicket", ($data) => {
                var noti = new AWN();
                noti.success("Hê lô.! Có lời mời kết bạn kìa !!!")
            });
        },

        sendMessage() {
            let mesg = document.querySelector("#mesg").value;
            //gửi dữ liệu
            //tham số đầu tiên là tên của function trên hub
            //tham số thứ 2 là data cần gửi
            this._connection.invoke("SendMessage", mesg);
            //xóa nội dung tin nhắn sau khí gửi
            document.querySelector("#mesg").value = "";
            document.querySelector("#mesg").focus();
        },
        findFriend() {
            let sreach = this.$refs.inputSearch.value;

            if (sreach.length < 3) {
                return;
            }

            let url = "/Friend/SearchAll/?keyword=" + sreach;
            fetch(url)
                .then(res => res.json())
                .then(json => {
                    this._searchFriend = json;
                });
        },

        addFriendRequest(id) {

            this._connection.invoke("AddFriend", Number(id));

        }
    }))
})

