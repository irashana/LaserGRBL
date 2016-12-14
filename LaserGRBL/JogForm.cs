﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaserGRBL
{
	public partial class JogForm : UserControls.DockingManager.DockContent
	{
		GrblCom ComPort;

		public JogForm(GrblCom com)
		{
			InitializeComponent();
			ComPort = com;
			TbSpeed_ValueChanged(null, null); //set tooltip
			TbStep_ValueChanged(null, null); //set tooltip
			TimerUpdate();
		}

		public void TimerUpdate()
		{
			SuspendLayout();
			foreach (Control ctr in tlp.Controls)
					ctr.Enabled = ComPort.JogEnabled;
			ResumeLayout();
		}

		private void OnJogButtonMouseDown(object sender, MouseEventArgs e)
		{
			ComPort.Jog((sender as DirectionButton).JogDirection, TbStep.Value, TbSpeed.Value);
		}

		private void TbSpeed_ValueChanged(object sender, EventArgs e)
		{
			TT.SetToolTip(TbSpeed, string.Format("Speed: {0}", TbSpeed.Value));
		}

		private void TbStep_ValueChanged(object sender, EventArgs e)
		{
			TT.SetToolTip(TbStep, string.Format("Step: {0}", TbStep.Value));
		}

		private void BtnHome_Click(object sender, EventArgs e)
		{
			ComPort.JogHome(TbSpeed.Value);
		}
	}

	public class DirectionButton : UserControls.ImageButton
	{
		private GrblCom.JogDirection mDir = GrblCom.JogDirection.N;

		public GrblCom.JogDirection JogDirection
		{
			get { return mDir; }
			set { mDir = value; }
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			if (Width != Height)
				Width = Height;

			base.OnSizeChanged(e);
		}
	}
}
