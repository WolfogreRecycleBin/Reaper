# Reaper（收割者） #

本项目是抓取天气信息过程中，为了方便，写的一个衍生工具，用于提取HTML代码中关键信息。因为感觉这个工具有极好的重复利用价值，故独立为一个项目。 
其前身为另一个为实现同样目的工具：Extractor，但很做了很大改进。

该工具特色有：
1. 一个Reaper对象，将对其保存的 基字符串组 进行提取操作，基字符串组 可以包含一个或多个字符串；
2. 提取操作返回的结果，是以提取结果为 基字符串组 的Reaper对象，故很容易写成链式表达式；
3. 实现了迭代器接口，可以方便得使用foreach语句。

详细的使用说明以后再补全。现在以如下简单的字符串为例说明Reaper的使用：

    <h>AAA</h>
    <l>aaa</l>
	<d>123</d>
    <d>564</d>
    <l>bbb</l>
	<d>122</d>
    <d>764</d>
	<l>ccc</l>
	<d>132</d>
    <d>754</d>
	<h>BBB</h>
    <l>aaa</l>
	<d>163</d>
    <d>324</d>
    <l>bbb</l>
	<d>022</d>
    <x>wolf</x>   //干扰项，不需要的。

该字符串试图说明如下结构
""""""""""AAA
"""""aaa
123
564
"""""bbb
122
764
"""""ccc
132
754
""""""""""BBB
"""""aaa
163
324
"""""bbb
022

为了方便程序读入，我们需要将数据处理为

    AAA
    aaa
    123
    
    AAA
    aaa
    564
    
    AAA
    bbb
    122
    略……

用Reaper处理的伪代码可以为：

    Reaper reaper = new Reaper(原字符串);
	foreach（Reaper heads in reaper.RemainBeforeFirst("<x>").ReapByProfix("<h>")
	{
		string  headName = heads.RemainBeforeFirst("</h>").GetResult()[0];
		foreach（Reaper lines in heads.ReapByProfix("<l>")
		{
			string lineName = lines.RemainBeforeFirst("</l>").GetResult()[0];
			foreach（Reaper datas in lines.ReapByProfix("<d>")
			{
				string data = heads.RemainBeforeFirst("</d>").GetResult()[0];
				输出(headName);
				输出(lineName);
				输出(data);
			}
		}
	}


目前的设计还不够完善，今后继续研究和优化。