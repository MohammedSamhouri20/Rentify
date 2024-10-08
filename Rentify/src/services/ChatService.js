export const startChat = async (connection, senderId, receiverId) => {
  return await connection.invoke("StartChatAsync", senderId, receiverId);
};

export const sendMessage = async (connection, chatId, text) => {
  return await connection.invoke("SendMessageAsync", chatId, text);
};
export const sendBookingMessage = async (connection, chatId, booking) => {
  return await connection.invoke("SendMessageBookingAsync", chatId, booking);
};

export const formatMessages = (messages, senderId) => {
  return messages.map((msg) => ({
    message: JSON.parse(msg.message),
    sender: msg.sender.userId,
    isSender: msg.sender.userId === senderId,
    sentAt: new Date(msg.sentAt).toLocaleTimeString([], {
      hour: "2-digit",
      minute: "2-digit",
    }),
  }));
};
