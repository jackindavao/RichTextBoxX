#RichTextBoxX

RichTextBoxX is a wrapper to add functionality to the RichTextBox control of VisualBasic.NET. It provides lower level methods for manipulating text and formatting, and higher level methods for various word processing functionality such as search, search and replace, etc. Included with it is a very simple GUI form illustrating how to incorporate the RichTextBoxX and use it as a simple word processor. This is not intended as a word processor for end users -- certainly there are better ones available -- what it is intended for is a kind of plugin for use in VB applications that need more word processing functionality than the bare RichTextBox provides. It also may be useful as example code for someone trying to figure out how to get some of the more obscure RichTextBox functionality to work in a predictable way -- in particular, how to handle the problems relating to specifying locations in text. Source code for Visual Studio 2010 and Windows installer are provided.
The source code for the RichTextBoxX wrapper is in RichTextBoxX.vb. To use simply pass a RichTextBox object to the RichTextBoxX constructor.