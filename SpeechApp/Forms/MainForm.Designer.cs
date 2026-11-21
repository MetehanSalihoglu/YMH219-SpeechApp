namespace SpeechApp.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        panelTop = new Panel();
        lblActivity = new Label();
        tbl = new TableLayoutPanel();
        grpTts = new GroupBox();
        rtbTts = new RichTextBox();
        pnlTtsBottom = new Panel();
        btnClearTts = new Button();
        btnStopTts = new Button();
        btnPauseTts = new Button();
        btnSpeak = new Button();
        lblVolume = new Label();
        trackVolume = new TrackBar();
        lblRate = new Label();
        trackRate = new TrackBar();
        cmbVoice = new ComboBox();
        lblVoice = new Label();
        grpStt = new GroupBox();
        lvHistory = new ListView();
        colTime = new ColumnHeader();
        colText = new ColumnHeader();
        colConf = new ColumnHeader();
        rtbStt = new RichTextBox();
        pnlSttTop = new Panel();
        btnClearStt = new Button();
        btnSaveStt = new Button();
        btnStopListen = new Button();
        btnListen = new Button();
        statusStrip = new StatusStrip();
        lblStatus = new ToolStripStatusLabel();
        saveDialog = new SaveFileDialog();
        panelTop.SuspendLayout();
        tbl.SuspendLayout();
        grpTts.SuspendLayout();
        pnlTtsBottom.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)trackVolume).BeginInit();
        ((System.ComponentModel.ISupportInitialize)trackRate).BeginInit();
        grpStt.SuspendLayout();
        pnlSttTop.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // panelTop
        // 
        panelTop.BackColor = Color.FromArgb(45, 52, 64);
        panelTop.Controls.Add(lblActivity);
        panelTop.Dock = DockStyle.Top;
        panelTop.Name = "panelTop";
        panelTop.Size = new Size(980, 52);
        panelTop.TabIndex = 0;
        // 
        // lblActivity
        // 
        lblActivity.BackColor = Color.Gray;
        lblActivity.Dock = DockStyle.Fill;
        lblActivity.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblActivity.ForeColor = Color.White;
        lblActivity.Name = "lblActivity";
        lblActivity.Text = "● AKTİF İŞLEM: Yok (Boşta)";
        lblActivity.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // tbl
        // 
        tbl.ColumnCount = 2;
        tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tbl.Controls.Add(grpTts, 0, 0);
        tbl.Controls.Add(grpStt, 1, 0);
        tbl.Dock = DockStyle.Fill;
        tbl.Name = "tbl";
        tbl.Padding = new Padding(6);
        tbl.RowCount = 1;
        tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tbl.TabIndex = 1;
        // 
        // grpTts
        // 
        grpTts.Controls.Add(rtbTts);
        grpTts.Controls.Add(pnlTtsBottom);
        grpTts.Dock = DockStyle.Fill;
        grpTts.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        grpTts.Name = "grpTts";
        grpTts.Padding = new Padding(8);
        grpTts.TabIndex = 0;
        grpTts.TabStop = false;
        grpTts.Text = "Metinden Sese (Text-to-Speech)";
        // 
        // rtbTts
        // 
        rtbTts.Dock = DockStyle.Fill;
        rtbTts.Font = new Font("Segoe UI", 11F);
        rtbTts.Name = "rtbTts";
        rtbTts.TabIndex = 0;
        rtbTts.Text = "Merhaba! Bu metin Windows konuşma motoru tarafından sesli okunacak. Hello, this text will be read aloud.";
        // 
        // pnlTtsBottom
        // 
        pnlTtsBottom.Controls.Add(btnClearTts);
        pnlTtsBottom.Controls.Add(btnStopTts);
        pnlTtsBottom.Controls.Add(btnPauseTts);
        pnlTtsBottom.Controls.Add(btnSpeak);
        pnlTtsBottom.Controls.Add(lblVolume);
        pnlTtsBottom.Controls.Add(trackVolume);
        pnlTtsBottom.Controls.Add(lblRate);
        pnlTtsBottom.Controls.Add(trackRate);
        pnlTtsBottom.Controls.Add(cmbVoice);
        pnlTtsBottom.Controls.Add(lblVoice);
        pnlTtsBottom.Dock = DockStyle.Bottom;
        pnlTtsBottom.Name = "pnlTtsBottom";
        pnlTtsBottom.Size = new Size(450, 170);
        pnlTtsBottom.TabIndex = 1;
        // 
        // lblVoice
        // 
        lblVoice.AutoSize = true;
        lblVoice.Font = new Font("Segoe UI", 9.5F);
        lblVoice.Location = new Point(2, 10);
        lblVoice.Name = "lblVoice";
        lblVoice.Text = "Ses:";
        // 
        // cmbVoice
        // 
        cmbVoice.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cmbVoice.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbVoice.Font = new Font("Segoe UI", 9.5F);
        cmbVoice.Location = new Point(70, 6);
        cmbVoice.Name = "cmbVoice";
        cmbVoice.Size = new Size(370, 25);
        cmbVoice.TabIndex = 1;
        cmbVoice.SelectedIndexChanged += cmbVoice_SelectedIndexChanged;
        // 
        // lblRate
        // 
        lblRate.AutoSize = true;
        lblRate.Font = new Font("Segoe UI", 9.5F);
        lblRate.Location = new Point(2, 46);
        lblRate.Name = "lblRate";
        lblRate.Text = "Hız: 0";
        // 
        // trackRate
        // 
        trackRate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        trackRate.Location = new Point(70, 40);
        trackRate.Maximum = 10;
        trackRate.Minimum = -10;
        trackRate.Name = "trackRate";
        trackRate.Size = new Size(370, 45);
        trackRate.TabIndex = 2;
        trackRate.Scroll += trackRate_Scroll;
        // 
        // lblVolume
        // 
        lblVolume.AutoSize = true;
        lblVolume.Font = new Font("Segoe UI", 9.5F);
        lblVolume.Location = new Point(2, 80);
        lblVolume.Name = "lblVolume";
        lblVolume.Text = "Ses düzeyi: 100";
        // 
        // trackVolume
        // 
        trackVolume.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        trackVolume.Location = new Point(110, 74);
        trackVolume.Maximum = 100;
        trackVolume.Name = "trackVolume";
        trackVolume.Size = new Size(330, 45);
        trackVolume.TabIndex = 3;
        trackVolume.TickFrequency = 10;
        trackVolume.Value = 100;
        trackVolume.Scroll += trackVolume_Scroll;
        // 
        // btnSpeak
        // 
        btnSpeak.BackColor = Color.FromArgb(46, 160, 67);
        btnSpeak.FlatStyle = FlatStyle.Flat;
        btnSpeak.ForeColor = Color.White;
        btnSpeak.Location = new Point(2, 124);
        btnSpeak.Name = "btnSpeak";
        btnSpeak.Size = new Size(100, 34);
        btnSpeak.TabIndex = 4;
        btnSpeak.Text = "▶ Başlat";
        btnSpeak.UseVisualStyleBackColor = false;
        btnSpeak.Click += btnSpeak_Click;
        // 
        // btnPauseTts
        // 
        btnPauseTts.Enabled = false;
        btnPauseTts.Location = new Point(108, 124);
        btnPauseTts.Name = "btnPauseTts";
        btnPauseTts.Size = new Size(110, 34);
        btnPauseTts.TabIndex = 5;
        btnPauseTts.Text = "⏸ Duraklat";
        btnPauseTts.UseVisualStyleBackColor = true;
        btnPauseTts.Click += btnPauseTts_Click;
        // 
        // btnStopTts
        // 
        btnStopTts.Enabled = false;
        btnStopTts.Location = new Point(224, 124);
        btnStopTts.Name = "btnStopTts";
        btnStopTts.Size = new Size(100, 34);
        btnStopTts.TabIndex = 6;
        btnStopTts.Text = "■ Durdur";
        btnStopTts.UseVisualStyleBackColor = true;
        btnStopTts.Click += btnStopTts_Click;
        // 
        // btnClearTts
        // 
        btnClearTts.Location = new Point(330, 124);
        btnClearTts.Name = "btnClearTts";
        btnClearTts.Size = new Size(100, 34);
        btnClearTts.TabIndex = 7;
        btnClearTts.Text = "Temizle";
        btnClearTts.UseVisualStyleBackColor = true;
        btnClearTts.Click += btnClearTts_Click;
        // 
        // grpStt
        // 
        grpStt.Controls.Add(lvHistory);
        grpStt.Controls.Add(rtbStt);
        grpStt.Controls.Add(pnlSttTop);
        grpStt.Dock = DockStyle.Fill;
        grpStt.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        grpStt.Name = "grpStt";
        grpStt.Padding = new Padding(8);
        grpStt.TabIndex = 1;
        grpStt.TabStop = false;
        grpStt.Text = "Sesten Metne (Speech-to-Text)";
        // 
        // pnlSttTop
        // 
        pnlSttTop.Controls.Add(btnClearStt);
        pnlSttTop.Controls.Add(btnSaveStt);
        pnlSttTop.Controls.Add(btnStopListen);
        pnlSttTop.Controls.Add(btnListen);
        pnlSttTop.Dock = DockStyle.Top;
        pnlSttTop.Name = "pnlSttTop";
        pnlSttTop.Size = new Size(450, 46);
        pnlSttTop.TabIndex = 0;
        // 
        // btnListen
        // 
        btnListen.BackColor = Color.FromArgb(46, 160, 67);
        btnListen.FlatStyle = FlatStyle.Flat;
        btnListen.ForeColor = Color.White;
        btnListen.Location = new Point(2, 6);
        btnListen.Name = "btnListen";
        btnListen.Size = new Size(120, 34);
        btnListen.TabIndex = 0;
        btnListen.Text = "🎤 Dinle";
        btnListen.UseVisualStyleBackColor = false;
        btnListen.Click += btnListen_Click;
        // 
        // btnStopListen
        // 
        btnStopListen.Enabled = false;
        btnStopListen.Location = new Point(128, 6);
        btnStopListen.Name = "btnStopListen";
        btnStopListen.Size = new Size(100, 34);
        btnStopListen.TabIndex = 1;
        btnStopListen.Text = "■ Durdur";
        btnStopListen.UseVisualStyleBackColor = true;
        btnStopListen.Click += btnStopListen_Click;
        // 
        // btnSaveStt
        // 
        btnSaveStt.Location = new Point(234, 6);
        btnSaveStt.Name = "btnSaveStt";
        btnSaveStt.Size = new Size(110, 34);
        btnSaveStt.TabIndex = 2;
        btnSaveStt.Text = "💾 .txt Kaydet";
        btnSaveStt.UseVisualStyleBackColor = true;
        btnSaveStt.Click += btnSaveStt_Click;
        // 
        // btnClearStt
        // 
        btnClearStt.Location = new Point(350, 6);
        btnClearStt.Name = "btnClearStt";
        btnClearStt.Size = new Size(90, 34);
        btnClearStt.TabIndex = 3;
        btnClearStt.Text = "Temizle";
        btnClearStt.UseVisualStyleBackColor = true;
        btnClearStt.Click += btnClearStt_Click;
        // 
        // rtbStt
        // 
        rtbStt.Dock = DockStyle.Top;
        rtbStt.Font = new Font("Segoe UI", 11F);
        rtbStt.Name = "rtbStt";
        rtbStt.Size = new Size(450, 150);
        rtbStt.TabIndex = 1;
        rtbStt.Text = "";
        // 
        // lvHistory
        // 
        lvHistory.Columns.AddRange(new ColumnHeader[] { colTime, colText, colConf });
        lvHistory.Dock = DockStyle.Fill;
        lvHistory.Font = new Font("Segoe UI", 9F);
        lvHistory.FullRowSelect = true;
        lvHistory.GridLines = true;
        lvHistory.Name = "lvHistory";
        lvHistory.TabIndex = 2;
        lvHistory.UseCompatibleStateImageBehavior = false;
        lvHistory.View = View.Details;
        // 
        // colTime
        // 
        colTime.Text = "Zaman";
        colTime.Width = 70;
        // 
        // colText
        // 
        colText.Text = "Tanınan metin";
        colText.Width = 270;
        // 
        // colConf
        // 
        colConf.Text = "Güven";
        colConf.Width = 60;
        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
        statusStrip.Name = "statusStrip";
        statusStrip.TabIndex = 2;
        // 
        // lblStatus
        // 
        lblStatus.Name = "lblStatus";
        lblStatus.Text = "Hazır.";
        // 
        // saveDialog
        // 
        saveDialog.DefaultExt = "txt";
        saveDialog.Filter = "Metin dosyası (*.txt)|*.txt";
        saveDialog.Title = "Dönüştürülen metni kaydet";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(980, 560);
        Controls.Add(tbl);
        Controls.Add(panelTop);
        Controls.Add(statusStrip);
        MinimumSize = new Size(900, 520);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Speech-to-Text / Text-to-Speech - YMH219";
        FormClosing += MainForm_FormClosing;
        Load += MainForm_Load;
        panelTop.ResumeLayout(false);
        tbl.ResumeLayout(false);
        grpTts.ResumeLayout(false);
        pnlTtsBottom.ResumeLayout(false);
        pnlTtsBottom.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)trackVolume).EndInit();
        ((System.ComponentModel.ISupportInitialize)trackRate).EndInit();
        grpStt.ResumeLayout(false);
        pnlSttTop.ResumeLayout(false);
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Panel panelTop;
    private Label lblActivity;
    private TableLayoutPanel tbl;
    private GroupBox grpTts;
    private RichTextBox rtbTts;
    private Panel pnlTtsBottom;
    private Label lblVoice;
    private ComboBox cmbVoice;
    private Label lblRate;
    private TrackBar trackRate;
    private Label lblVolume;
    private TrackBar trackVolume;
    private Button btnSpeak;
    private Button btnPauseTts;
    private Button btnStopTts;
    private Button btnClearTts;
    private GroupBox grpStt;
    private Panel pnlSttTop;
    private Button btnListen;
    private Button btnStopListen;
    private Button btnSaveStt;
    private Button btnClearStt;
    private RichTextBox rtbStt;
    private ListView lvHistory;
    private ColumnHeader colTime;
    private ColumnHeader colText;
    private ColumnHeader colConf;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel lblStatus;
    private SaveFileDialog saveDialog;
}
