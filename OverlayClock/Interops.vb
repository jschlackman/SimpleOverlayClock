Public Class Interops

    Public Const HWND_TOPMOST = -1
    Public Const HWND_NOTOPMOST = -2
    Public Const SWP_NOMOVE = &H2
    Public Const SWP_NOSIZE = &H1
    Public Const SWP_NOACTIVATE = &H10
    Public Const SWP_SHOWWINDOW = &H40
    Public Const FLAGS = SWP_NOMOVE Or SWP_NOSIZE

    ''' <summary>
    ''' Changes the size, position, and Z order of a child, pop-up, or top-level window. These windows are ordered according to their appearance on the screen. The topmost window receives the highest rank and is the first window in the Z order.
    ''' </summary>
    ''' <param name="hwnd">A handle to the window.</param>
    ''' <param name="hWndInsertAfter">A handle to the window to precede the positioned window in the Z order, or defined values indicating HWND_BOTTOM, HWND_NOTOPMOST, HWND_TOP, HWND_TOPMOST.</param>
    ''' <param name="x">The new position of the left side of the window, in client coordinates. </param>
    ''' <param name="y">The new position of the top of the window, in client coordinates. </param>
    ''' <param name="cx">The new width of the window, in pixels.</param>
    ''' <param name="cy">The new height of the window, in pixels.</param>
    ''' <param name="wFlags">The window sizing and positioning flags.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Declare Function SetWindowPos Lib "user32.dll" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Boolean

End Class
