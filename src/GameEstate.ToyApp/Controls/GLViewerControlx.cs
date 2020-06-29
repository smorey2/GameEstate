//using GameEstate.Toy.Models;
//using GameEstate.Toy.Renderer;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace GameEstate.Toy.Controls
//{
//    public class GLViewerControl : GLBaseControl
//    {
//        readonly List<Label> labels = new List<Label>();
//        readonly List<UserControl> selectionBoxes = new List<UserControl>();
//        readonly List<Control> otherControls = new List<Control>();

//        private void SetFps(double fps)
//        {
//            fpsLabel.Text = $"FPS: {Math.Round(fps).ToString(CultureInfo.InvariantCulture)}";
//        }

//        public Label AddLabel(string text)
//        {
//            var label = new Label();
//            label.Text = text;
//            label.AutoSize = true;

//            controlsPanel.Controls.Add(label);

//            labels.Add(label);

//            RecalculatePositions();

//            return label;
//        }

//        public void AddControl(Control control)
//        {
//            controlsPanel.Controls.Add(control);
//            otherControls.Add(control);
//            RecalculatePositions();
//        }

//        public CheckBox AddCheckBox(string name, bool defaultChecked, Action<bool> changeCallback)
//        {
//            var checkbox = new GLViewerCheckboxControl(name, defaultChecked);
//            checkbox.CheckBox.CheckedChanged += (_, __) =>
//            {
//                changeCallback(checkbox.CheckBox.Checked);

//                GLControl.Focus();
//            };

//            controlsPanel.Controls.Add(checkbox);
//            otherControls.Add(checkbox);

//            RecalculatePositions();

//            return checkbox.CheckBox;
//        }

//        public ComboBox AddSelection(string name, Action<string, int> changeCallback)
//        {
//            var selectionControl = new GLViewerSelectionControl(name);

//            controlsPanel.Controls.Add(selectionControl);
//            selectionBoxes.Add(selectionControl);

//            selectionControl.PerformAutoScale();

//            RecalculatePositions();

//            selectionControl.ComboBox.SelectionChangeCommitted += (_, __) =>
//            {
//                selectionControl.Refresh();
//                changeCallback(selectionControl.ComboBox.SelectedItem as string, selectionControl.ComboBox.SelectedIndex);

//                GLControl.Focus();
//            };

//            return selectionControl.ComboBox;
//        }

//        public CheckedListBox AddMultiSelection(string name, Action<IEnumerable<string>> changeCallback)
//        {
//            var selectionControl = new GLViewerMultiSelectionControl(name);

//            controlsPanel.Controls.Add(selectionControl);
//            selectionBoxes.Add(selectionControl);

//            selectionControl.PerformAutoScale();

//            RecalculatePositions();

//            selectionControl.CheckedListBox.ItemCheck += (_, __) =>
//            {
//                // ItemCheck is called before CheckedItems is updated
//                BeginInvoke((MethodInvoker)(() =>
//                {
//                    selectionControl.Refresh();
//                    changeCallback(selectionControl.CheckedListBox.CheckedItems.OfType<string>());

//                    GLControl.Focus();
//                }));
//            };

//            return selectionControl.CheckedListBox;
//        }

//        public void RecalculatePositions()
//        {
//            var y = 25;

//            foreach (var label in labels)
//            {
//                label.Location = new Point(0, y);
//                y += label.Height;
//            }

//            foreach (var selection in selectionBoxes)
//            {
//                selection.Location = new Point(0, y);
//                y += selection.Height;
//            }

//            foreach (var control in otherControls)
//            {
//                control.Location = new Point(0, y);
//                control.Width = glControlContainer.Location.X;
//                y += control.Height;
//            }
//        }

//       

//        private void Draw()
//        {
//            if (GLControl.Visible)
//            {
//                var frameTime = stopwatch.ElapsedMilliseconds / 1000f;
//                stopwatch.Restart();

//                Camera.Tick(frameTime);
//                Camera.HandleInput(Mouse.GetState(), Keyboard.GetState());

//                SetFps(1f / frameTime);

//                GL.ClearColor(Settings.BackgroundColor);
//                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

//                GLPaint?.Invoke(this, new RenderEventArgs { FrameTime = frameTime, Camera = Camera });

//                GLControl.SwapBuffers();
//                GLControl.Invalidate();
//            }
//        }
//    }
//}
