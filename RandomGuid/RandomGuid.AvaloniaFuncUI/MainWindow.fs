namespace RandomGuid.AvaloniaFuncUI

open System
open Avalonia
open Avalonia.Controls
open Avalonia.FuncUI.DSL
open Avalonia.Layout
open Avalonia.Media

module PrimaryWindow =

    type State = { GuidString: string }
    let init = { GuidString = "" }

    // Events
    type Msg =
        | NewGuid
        | AppendGuid
        | CopyGuid
        | ClearGuid
        | TextChanged of text: string

    let update (msg: Msg) (state: State) =
        match msg with
        | NewGuid ->
            { state with
                  GuidString = Guid.NewGuid().ToString() }
        | AppendGuid ->
            { state with
                  GuidString = state.GuidString + Guid.NewGuid().ToString() }
        | CopyGuid ->
            Application.Current.Clipboard.SetTextAsync(state.GuidString)
            |> ignore

            state
        | ClearGuid -> init
        | TextChanged (text) -> { state with GuidString = text }

    let view (state: State) (dispatch: Msg -> unit) =
        StackPanel.create [ StackPanel.horizontalAlignment HorizontalAlignment.Stretch
                            StackPanel.verticalAlignment VerticalAlignment.Center
                            StackPanel.children [ TextBox.create [ TextBox.name "guidStringTextBox"
                                                                   TextBox.textWrapping TextWrapping.Wrap
                                                                   TextBox.acceptsReturn true
                                                                   TextBox.onTextChanged
                                                                       (fun x ->
                                                                           if x <> state.GuidString then
                                                                               dispatch (TextChanged x))
                                                                   TextBox.text state.GuidString ]
                                                  StackPanel.create [ StackPanel.horizontalAlignment
                                                                          HorizontalAlignment.Center
                                                                      StackPanel.orientation Orientation.Horizontal
                                                                      StackPanel.margin (new Thickness(10.0))
                                                                      StackPanel.children [ Button.create [ Button.content
                                                                                                                "New"
                                                                                                            Button.margin (
                                                                                                                new Thickness(
                                                                                                                    0.0,
                                                                                                                    0.0,
                                                                                                                    3.0,
                                                                                                                    0.0
                                                                                                                )
                                                                                                            )
                                                                                                            Button.height
                                                                                                                30.0
                                                                                                            Button.width
                                                                                                                75.0
                                                                                                            Button.tip
                                                                                                                "New random text"
                                                                                                            Button.onClick
                                                                                                                (fun _ ->
                                                                                                                    dispatch
                                                                                                                        NewGuid) ]
                                                                                            Button.create [ Button.content
                                                                                                                "Append"
                                                                                                            Button.margin (
                                                                                                                new Thickness(
                                                                                                                    0.0,
                                                                                                                    0.0,
                                                                                                                    3.0,
                                                                                                                    0.0
                                                                                                                )
                                                                                                            )
                                                                                                            Button.height
                                                                                                                30.0
                                                                                                            Button.width
                                                                                                                75.0
                                                                                                            Button.tip
                                                                                                                "Add random text to the currently available"
                                                                                                            Button.onClick
                                                                                                                (fun _ ->
                                                                                                                    dispatch
                                                                                                                        AppendGuid) ]
                                                                                            Button.create [ Button.content
                                                                                                                "Copy"
                                                                                                            Button.margin (
                                                                                                                new Thickness(
                                                                                                                    0.0,
                                                                                                                    0.0,
                                                                                                                    3.0,
                                                                                                                    0.0
                                                                                                                )
                                                                                                            )
                                                                                                            Button.height
                                                                                                                30.0
                                                                                                            Button.width
                                                                                                                75.0
                                                                                                            Button.tip
                                                                                                                "Copy to clipboard"
                                                                                                            Button.onClick
                                                                                                                (fun _ ->
                                                                                                                    dispatch
                                                                                                                        CopyGuid) ]
                                                                                            Button.create [ Button.content
                                                                                                                "Clear"
                                                                                                            Button.margin (
                                                                                                                new Thickness(
                                                                                                                    0.0,
                                                                                                                    0.0,
                                                                                                                    3.0,
                                                                                                                    0.0
                                                                                                                )
                                                                                                            )
                                                                                                            Button.height
                                                                                                                30.0
                                                                                                            Button.width
                                                                                                                75.0
                                                                                                            Button.tip
                                                                                                                "Clear textbox"
                                                                                                            Button.onClick
                                                                                                                (fun _ ->
                                                                                                                    dispatch
                                                                                                                        ClearGuid) ]

                                                                                             ]

                                                                       ]

                                                   ]

                             ]
