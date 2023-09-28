using Myra;
using Myra.Graphics2D.UI;
using System;
using System.IO;

namespace Boids
{
    class UI
    {
        private Desktop _desktop;
        public bool Visible => _desktop.Root.Visible;

        public UI(BoidGame game, BoidManager boidManager)
        {
            LoadUIAndSettings(game, boidManager);
        }

        public void Draw()
        {
            _desktop.Render();
        }

        public void ToggleVisibility()
        {
            _desktop.Root.Visible = !_desktop.Root.Visible;
        }

        private void LoadUIAndSettings(BoidGame game, BoidManager boidManager)
        {
            MyraEnvironment.Game = game;
            string importData = File.ReadAllText("ui.xml");
            var project = Project.LoadFromXml(importData, MyraEnvironment.DefaultAssetManager);

            HorizontalSlider cohesionSlider = project.Root.FindWidgetById("CohesionSlider") as HorizontalSlider;
            Label cohesionValueLabel = project.Root.FindWidgetById("CohesionValueLabel") as Label;
            cohesionSlider.ValueChanged += (src, value) =>
            {
                cohesionValueLabel.Text = value.NewValue.ToString();
                Config.CohesionFactor = value.NewValue;
            };

            HorizontalSlider seperationSlider = project.Root.FindWidgetById("SeperationSlider") as HorizontalSlider;
            Label seperationValueLabel = project.Root.FindWidgetById("SeperationValueLabel") as Label;
            seperationSlider.ValueChanged += (src, value) =>
            {
                seperationValueLabel.Text = value.NewValue.ToString();
                Config.SeperationFactor = value.NewValue;
            };

            HorizontalSlider seperationMinDistanceSlider = project.Root.FindWidgetById("SeperationDistSlider") as HorizontalSlider;
            Label seperationMinDistValueLabel = project.Root.FindWidgetById("SeperationDistValueLabel") as Label;
            seperationMinDistanceSlider.ValueChanged += (src, value) =>
            {
                seperationMinDistValueLabel.Text = value.NewValue.ToString();
                Config.SeperationMinDistance = value.NewValue;
            };

            HorizontalSlider groupAlignmentSlider = project.Root.FindWidgetById("GroupAlignmentSlider") as HorizontalSlider;
            Label groupAlignmentValueLabel = project.Root.FindWidgetById("GroupAlignmentValueLabel") as Label;
            groupAlignmentSlider.ValueChanged += (src, value) =>
            {
                groupAlignmentValueLabel.Text = value.NewValue.ToString();
                Config.GroupAlignmentFactor = value.NewValue;
            };

            HorizontalSlider boundsMarginSlider = project.Root.FindWidgetById("BoundsMarginSlider") as HorizontalSlider;
            Label boundsMarginValueLabel = project.Root.FindWidgetById("BoundsMarginValueLabel") as Label;
            boundsMarginSlider.ValueChanged += (src, value) =>
            {
                boundsMarginValueLabel.Text = value.NewValue.ToString();
                Config.BoundsMargin = value.NewValue;
            };

            HorizontalSlider boundsRepelSlider = project.Root.FindWidgetById("BoundsRepelSlider") as HorizontalSlider;
            Label boundsRepelValueLabel = project.Root.FindWidgetById("BoundsRepelValueLabel") as Label;
            boundsRepelSlider.ValueChanged += (src, value) =>
            {
                boundsRepelValueLabel.Text = value.NewValue.ToString();
                Config.BoundRepelFactor = value.NewValue;
            };

            HorizontalSlider boidVisionSlider = project.Root.FindWidgetById("BoidVisionSlider") as HorizontalSlider;
            Label boidVisionValueLabel = project.Root.FindWidgetById("BoidVisonValueLabel") as Label;
            boidVisionSlider.ValueChanged += (src, value) =>
            {
                boidVisionValueLabel.Text = value.NewValue.ToString();
                Config.BoidVision = value.NewValue;
            };

            HorizontalSlider maxSpeedSlider = project.Root.FindWidgetById("MaxSpeedSlider") as HorizontalSlider;
            Label maxSpeedValueLabel = project.Root.FindWidgetById("MaxSpeedValueLabel") as Label;
            maxSpeedSlider.ValueChanged += (src, value) =>
            {
                maxSpeedValueLabel.Text = value.NewValue.ToString();
                Config.MaxSpeed = value.NewValue;
            };

            HorizontalSlider boidCountSlider = project.Root.FindWidgetById("BoidCountSlider") as HorizontalSlider;
            Label boidCountValueLabel = project.Root.FindWidgetById("BoidCountValueLabel") as Label;
            boidCountSlider.ValueChanged += (src, value) =>
            {
                boidCountValueLabel.Text = ((int)value.NewValue).ToString();
                Config.BoidCount = (int)value.NewValue;
            };

            HorizontalSlider destinationAlignmentSlider = project.Root.FindWidgetById("DestinationAlignmentSlider") as HorizontalSlider;
            Label destAlignmentValueLabel = project.Root.FindWidgetById("DestinationAlignmentValueLabel") as Label;
            destinationAlignmentSlider.ValueChanged += (src, value) =>
            {
                destAlignmentValueLabel.Text = value.NewValue.ToString();
                Config.DestinationFactor = value.NewValue;
            };

            HorizontalSlider arrivalSpeedClampSlider = project.Root.FindWidgetById("ArrivalSpeedClampSlider") as HorizontalSlider;
            Label arrivalSpeedClampValueLabel = project.Root.FindWidgetById("ArrivalSpeedClampValueLabel") as Label;
            arrivalSpeedClampSlider.ValueChanged += (src, value) =>
            {
                arrivalSpeedClampValueLabel.Text = value.NewValue.ToString();
                Config.ArrivalSpeedLimit = value.NewValue;
            };

            HorizontalSlider arrivalDragSlider = project.Root.FindWidgetById("ArrivalDragSlider") as HorizontalSlider;
            Label arrivalDragValueLabel = project.Root.FindWidgetById("ArrivalDragValueLabel") as Label;
            arrivalDragSlider.ValueChanged += (src, value) =>
            {
                arrivalDragValueLabel.Text = value.NewValue.ToString();
                Config.ArrivalDrag = value.NewValue;
            };

            HorizontalSlider collisionRepelSlider = project.Root.FindWidgetById("CollisionRepelSlider") as HorizontalSlider;
            Label collisionRepelValueLabel = project.Root.FindWidgetById("CollisionRepelValueLabel") as Label;
            collisionRepelSlider.ValueChanged += (src, value) =>
            {
                collisionRepelValueLabel.Text = value.NewValue.ToString();
                Config.CollisionRepelMultiplier = value.NewValue;
            };

            HorizontalSlider avoidanceSlider = project.Root.FindWidgetById("AvoidanceSlider") as HorizontalSlider;
            Label avoidanceValueLabel = project.Root.FindWidgetById("AvoidanceValueLabel") as Label;
            avoidanceSlider.ValueChanged += (src, value) =>
            {
                avoidanceValueLabel.Text = value.NewValue.ToString();
                Config.AvoidanceFactor = value.NewValue;
            };

            HorizontalSlider avoidanceMinDistSlider = project.Root.FindWidgetById("AvoidanceDistanceSlider") as HorizontalSlider;
            Label avoidanceMinDistValueLabel = project.Root.FindWidgetById("AvoidanceDistanceValueLabel") as Label;
            avoidanceMinDistSlider.ValueChanged += (src, value) =>
            {
                avoidanceMinDistValueLabel.Text = value.NewValue.ToString();
                Config.AvoidanceMinDistance = value.NewValue;
            };

            var resetBtn = project.Root.FindWidgetById("ResetBtn") as ImageTextButton;
            resetBtn.Click += (src, value) =>
            {
                boidManager.Reset();
            };

            var toggleCollisionsBtn = project.Root.FindWidgetById("ToggleCollisonsBtn") as ImageTextButton;
            toggleCollisionsBtn.Click += (src, value) =>
            {
                Config.CollisionsEnabled = !Config.CollisionsEnabled;
                toggleCollisionsBtn.Text = Config.CollisionsEnabled ? "True" : "False";
            };

            var exportBtn = project.Root.FindWidgetById("ExportBtn") as ImageTextButton;
            exportBtn.Click += (src, value) =>
            {
                string exportString = "";
                exportString += $"CohesionFactor: {Config.CohesionFactor}\n";
                exportString += $"AvoidanceFactor: {Config.SeperationFactor}\n";
                exportString += $"AvoidanceMinDistance: {Config.SeperationMinDistance}\n";
                exportString += $"BoundMargin: {Config.BoundsMargin}\n";
                exportString += $"BoundsRepelFactor: {Config.BoundRepelFactor}\n";
                exportString += $"GroupAlignmentFactor: {Config.GroupAlignmentFactor}\n";
                exportString += $"DestinationFactor: {Config.DestinationFactor}\n";
                exportString += $"MaxSpeed: {Config.MaxSpeed}\n";
                exportString += $"Arrival Drag: {Config.ArrivalDrag}\n";
                exportString += $"Arrival Speed Clamp Limit: {Config.ArrivalSpeedLimit}\n";
                exportString += $"CollisionsEnabled: {Config.CollisionsEnabled}\n";
                exportString += $"BoidCount: {Config.BoundRepelFactor}\n";
                exportString += $"BoidVision: {Config.BoidVision}\n";
                File.WriteAllText("configExport.txt", exportString);
            };

            // Add it to the desktop
            _desktop = new Desktop();
            _desktop.Root = project.Root;
            _desktop.Root.Visible = false;
        }
    }


}
