' Simple Overlay Clock
' Copyright(C) 2017 J. Schlackman

' This program Is free software: you can redistribute it And/Or modify
' it under the terms Of the GNU General Public License As published by
' the Free Software Foundation, either version 3 Of the License, Or
' (at your option) any later version.

' This program Is distributed In the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty Of
' MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License For more details.

' You should have received a copy Of the GNU General Public License
' along with this program.  If Not, see <http://www.gnu.org/licenses/>.

Public Class Clock

    ''' <summary>
    ''' Font name to use for the clock
    ''' </summary>
    Const sSystemFont As String = "MS Shell Dlg"

    ''' <summary>
    ''' Fraction of screen to use for clock width
    ''' </summary>
    Const iWidthScale As Single = 1 / 6
    ''' <summary>
    ''' How far down the screen the bottom of the clock should be as a fraction of total screen height
    ''' </summary>
    Const iHeightScale As Single = 0.95
    ''' <summary>
    ''' Opacity of clock window
    ''' </summary>
    Const iOpacity As Single = 0.8

    ''' <summary>
    ''' Longest time string (assumes 0 is the widest character in the font and that 2 is wider than 1)
    ''' </summary>
    Private sTimeTest As String = DateTime.Parse("22:00").ToShortTimeString

    ''' <summary>
    ''' Stores the last date we checked font sizes for
    ''' </summary>
    Private dLastDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' Get a font with a size that will fit the clock window based on the current date
    ''' </summary>
    ''' <returns>Font format object.</returns>
    Private Function GetDateFont() As Font

        Dim iDateWidth As Integer
        Dim iFontSize As Integer = 6
        Dim fSystem As Font
        Dim sDateTest As String = Now.ToLongDateString

        'Try font sizes until we get one that is just too big
        Do
            iFontSize += 1
            fSystem = New Font(sSystemFont, iFontSize, FontStyle.Bold)
            iDateWidth = Me.CreateGraphics.MeasureString(sDateTest, fSystem).ToSize.Width

        Loop Until iDateWidth > lblDate.Width

        'Drop down a couple of point sizes
        fSystem = New Font(sSystemFont, Decimal.ToSingle(iFontSize * 0.9), FontStyle.Bold)

        Return fSystem

    End Function

    ''' <summary>
    ''' Get a font with a size that will fit the clock window based on the largest possible time string
    ''' </summary>
    ''' <returns></returns>
    Private Function GetTimeFont() As Font

        Dim iTimeWidth As Integer
        Dim iFontSize As Integer = 9
        Dim fSystem As Font

        'Try font sizes until we get one that is just too big
        Do
            iFontSize += 1
            fSystem = New Font(sSystemFont, iFontSize, FontStyle.Bold)
            iTimeWidth = Me.CreateGraphics.MeasureString(sTimeTest, fSystem).ToSize.Width

        Loop Until iTimeWidth > lblTime.Width

        'Drop down a couple of point sizes
        fSystem = New Font(sSystemFont, Decimal.ToSingle(iFontSize * 0.95), FontStyle.Bold)

        Return fSystem

    End Function

    ''' <summary>
    ''' Sets the clock window size according to the current screen size and system date/time formats.
    ''' </summary>
    Private Sub ResizeClock()
        'Get screen size
        Dim rScreen As Rectangle = Screen.PrimaryScreen.Bounds

        'Scale clock to screen size
        Me.Width = Int(rScreen.Width * iWidthScale)
        Me.Left = rScreen.Left + rScreen.Width - Me.Width

        'Get the time font and scale control
        lblTime.Font = GetTimeFont()
        lblTime.Height = Int((Me.CreateGraphics.MeasureString(sTimeTest, lblTime.Font).ToSize.Height) * 0.8)

        'Get the date font and scale control
        lblDate.Font = GetDateFont()
        lblDate.Height = Me.CreateGraphics.MeasureString(Now.ToLongDateString, lblDate.Font).ToSize.Height

        'Position the date control correctly
        lblDate.Top = lblTime.Bottom

        'Set the clock height correctly
        Me.Height = lblDate.Bottom + 10
        Me.Top = Int(rScreen.Top + rScreen.Height * iHeightScale) - Me.Height

        'Set translucent
        Me.Opacity = iOpacity

        Interops.SetWindowPos(Me.Handle, Interops.HWND_TOPMOST, Me.Left, Me.Top, Me.Width, Me.Height, Interops.FLAGS)

    End Sub

    ''' <summary>
    ''' Updates the clock window with the current date and time
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateTime()
        'If the date changed, set new clock size information
        If Now.Date > dLastDate Then ResizeClock()

        lblTime.Text = Now.ToShortTimeString
        lblDate.Text = Today.ToLongDateString
    End Sub

    Private Sub Clock_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        UpdateTime()
    End Sub

    Private Sub tmClock_Tick(sender As System.Object, e As System.EventArgs) Handles tmClock.Tick
        UpdateTime()

    End Sub

    Private Sub mniExit_Click(sender As System.Object, e As System.EventArgs) Handles mniExit.Click
        Me.Close()
    End Sub

    Private Sub mniAbout_Click(sender As System.Object, e As System.EventArgs) Handles mniAbout.Click
        About.ShowDialog()
    End Sub
End Class
