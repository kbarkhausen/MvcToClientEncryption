# Mvc To Client Encryption

An example on how to encrypt data sent from C# MVC to UI JavaScript app

If you write JavaScript code that depends on data received from the C# MVC, you should be careful that the JavaScript app does not fully trust the data it received from the C# MVC back-end.

When you send data from C# MVC back-end to the JS in the client app, you no longer control the data. The data is now in the browser memory storage.

Users can try to confuse your JS client app by editing the data stored in the browser memory in order to make your JS app behave in ways that it should not.

This repo shows a simple way to make passing data from the C# MVC to the JavaScript app by encrypting the data being sent.

**Using this encryption WILL NOT secure your data. It simply makes it harder for someone to inspect and manipulate the data your JavaScript app is receiving from the C# MVC back-end.**

Please do not rely on this approach to consider your JavaScript app fully secure. Test your JavaScript app for possible data manipulation.
