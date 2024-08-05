# World-Generator
个人柏林噪声项目

Personal project about Perlin-noise

セルフ利用ためのパーリン騒音プロジェクト

# Describe
这是一个尝试使用2d柏林噪声生成随机地形的个人Unity/C#项目。
2d-柏林噪声和一些关键的噪声利用代码代码由C#编写并打包为dll文件配置在Plugins文件夹内。
其详细内容请见Plugins文件下"Map generator.txt"。

值得注意的是我编写的柏林噪声生成代码与Ken Perlin先生的源码略有不同，我尝试通过旋转矩阵对原方法中的晶格顶点的固定梯度进行旋转以增加生成世界的随机性。
然而由于插值计算的投影方向改变这会使噪声生成的范围出现浮动，不再是(-1.0~1.0)。

注意:该项目为个人为了解柏林噪声而尝试试作版本，上述代码仅供参考，请勿直接用于正式项目。
如果您对柏林噪声有疑惑或是见解，欢迎留言或致信"chaokedsky@gmail.com"。
我会向您更详细的阐述柏林噪声的基础原理和我从该项目思考而得的其他考量。

This is a personal Unity/C# project that attempts to generate random terrain using 2d-Perlin noise.
2D-Perlin Noise and some of the key noise exploit code, written by C# and packaged as dll files, configured in the Plugins folder.
For more information, please refer to "Map generator.txt" under the Plugins file.

It is worth noting that the Perlin noise generation code I wrote is slightly different from Ken Perlin's source code, and I tried to rotate the fixed gradient of the lattice vertices in the original method by rotating matrix to increase the randomness of the generated world.
However, due to the change of the projection direction of the interpolation calculation, the range of noise generation will fluctuate, and it is no longer (-1.0~1.0).

Note: This project is a trial version of an attempt by individuals to understand Berlin noise, and the above code is only for reference and should not be used directly for a formal project.
If you have any questions or comments about Perlin noise, please leave a message or send email to "chaokedsky@gmail.com".
I will explain to you in more detail of Perlin noise algorithm　and the other knowledge I have got from this project.

2D-Perlinノイズを使用してランダムな地形を生成しようとする個人のUnity/C# プロジェクトです。
2D-Perlin Noise と一部の主要なノイズ エクスプロイト コードは、C# によって記述され、dll ファイルとしてパッケージ化され、Plugins フォルダーに構成されています。
詳細については、Pluginsファイルの「Map generator.txt」を参照してください。

なお、筆者が書いたPerlinノイズ生成コードはKen Perlin氏のソースコードとは若干異なり、生成ワールドのランダム性を高めるために、元の方法で格子頂点の固定勾配に行列回転をさせてみました。
しかし、内積計算の投影方向が変わったことにより、ノイズの生成範囲が変動し、-1.0~1.0ではなくなりました。

注:このプロジェクトは、ベルリンの騒音を理解するための個人による試みの試作品です。
上記のコードは参照用であり、正式なプロジェクトに直接使用しないでください。
Perlinノイズについて質問やコメントがある場合は、メッセージを残すか「chaokedsky@gmail.com」にメールを送信してください。
Perlinノイズアルゴリズムと、このプロジェクトから得たその他の知識について詳しく説明します。

# Use caution
1、请勿生成过大地图，在地图大小5×5~10×10 等比插值比率5倍或10倍 范围中生成为好。

2、当生成5*5,5倍插值比率地图时将生成8405个方块。

3、生成后可以使用Q/E键旋转观察地块。

1、Do not generate too large map, it is better to generate in the range of the interpolation ratio 5 times or 10 times of the map size 5×5~10×10.

2、When generating a 5×5, 5x interpolation ratio map, 8405 blocks will be generated.

3、After spawning, you can use the Q/E keys to rotate the observation plot.

1、大きすぎるマップを生成しないでください、マップサイズ5×5~10×10、線形値挿入比率の5倍または10倍の範囲で生成するのが良いでしょう。

2、5*5、5x補間比マップを生成すると、8405ブロックが生成されます。

3、生成した後、Q/Eキーを使用して観測プロットを回転させることができます。

# New release (last update 24/08/05)
如果您想获取 Unity 打包生成后的版本请至Release，下载V1.0.0-alpha中的World_Generator_v1.0.0.zip。

If you'd like to get the build Unity package, go to Release and download the World_Generator_v1.0.0.zip from v1.0.0-alpha

ビルド Unity パッケージを入手する場合は、リリースに移動し、v1.0.0-alpha からWorld_Generator_v1.0.0.zipをダウンロードしてください。

