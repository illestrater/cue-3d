using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class MusicStreamer : MonoBehaviour
{
	private int stream;
	private float[] channelData = new float[2048];
	System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

	public enum flags {
		BASS_SAMPLE_FLOAT=256,
		BASS_DATA_FFT4096=-2147483644
	}

	public enum configs {
		BASS_CONFIG_NET_PLAYLIST=1
	}

	public enum fftFlags {

	}

	[DllImport("bass")]
	public static extern bool BASS_SetConfig(configs config,int valuer);
	[DllImport("bass")]
	public static extern bool BASS_Init(int device, int freq, int flag,IntPtr hwnd, IntPtr clsid);
	[DllImport("bass")]
	public static extern Int32 BASS_StreamCreateURL(string url,int offset,   flags flag,  IntPtr user);

	[DllImport("bass")]
	public static extern bool BASS_ChannelPlay(int stream,bool restart);

	[DllImport("bass")]
	public static extern bool BASS_ChannelGetInfo(int stream,bool restart);

	[DllImport("bass")]
	public static extern int BASS_ChannelSeconds2Bytes(int stream,double pos);

	[DllImport("bass")]
	public static extern int BASS_ChannelGetData(int stream, float[] buffer, int length);

	[DllImport("bass")]
	public static extern bool BASS_StreamFree(int stream);
	
	[DllImport("bass")]
	public static extern bool BASS_Free();

	[DllImport("bass")]
	public static extern int BASS_ErrorGetCode();

	[DllImport("__Internal")]
	private static extern void play();

	[DllImport("__Internal")]
	private static extern void renderChart();

	[DllImport("__Internal")]
	private static extern string getFrequencyData();

	[DllImport("__Internal")]
	private static extern string hackWebGLKeyboard();
	
	void Start ()
	{
		#if !UNITY_EDITOR && UNITY_WEBGL
//		WebGLInput.captureAllKeyboardInput = false;
		play ();
		renderChart ();
//		hackWebGLKeyboard ();
		#endif

		#if UNITY_EDITOR
		if ( BASS_Init(-1, 44100,0,IntPtr.Zero, IntPtr.Zero) )
		{
			BASS_SetConfig(configs.BASS_CONFIG_NET_PLAYLIST,2);
			stream = BASS_StreamCreateURL("http://127.0.0.1:8000/stream.ogg", 0, flags.BASS_SAMPLE_FLOAT, IntPtr.Zero);

			if (stream != 0){
				BASS_ChannelPlay(stream, false);
				print ("playing");
			} else{
				print ("BASS Error: " + BASS_ErrorGetCode());
			}
			stopwatch.Start();
		}
		#endif
	}

	void Update ()
	{
		#if UNITY_EDITOR
		BASS_ChannelGetData(stream, this.channelData, (int)flags.BASS_DATA_FFT4096);

		float[] mapData = new float[22];
		for(int i = 0; i < 22; i++){
			mapData[i] = 5*(int)(Math.Sqrt ((double)this.channelData [i]) * 100);
		}
		#endif

		#if !UNITY_EDITOR && UNITY_WEBGL
		float[] mapData = new float[22];
		string audioVisualizerData = getFrequencyData();
		string[] dataString = audioVisualizerData.Split(',');
		int length = dataString.Length;
		for (int i = 0; i < length; i++) {
			mapData[i] = float.Parse(dataString[i]);
		}
		#endif

		GameObject[] cubes = GameObject.FindGameObjectsWithTag ("Cubes");
		float x = cubes [0].transform.localScale.x;
		float z = cubes [0].transform.localScale.z;
		for (int i = 0; i < cubes.Length; i++) {
			switch(cubes[i].name){
				case "c1": cubes[i].transform.localScale = new Vector3(x, mapData[0], z); break;
				case "c2": cubes[i].transform.localScale = new Vector3(x, mapData[1], z); break;
				case "c3": cubes[i].transform.localScale = new Vector3(x, mapData[2], z); break;
				case "c4": cubes[i].transform.localScale = new Vector3(x, mapData[3], z); break;
				case "c5": cubes[i].transform.localScale = new Vector3(x, mapData[4], z); break;
				case "c6": cubes[i].transform.localScale = new Vector3(x, mapData[5], z); break;
				case "c7": cubes[i].transform.localScale = new Vector3(x, mapData[6], z); break;
				case "c8": cubes[i].transform.localScale = new Vector3(x, mapData[7], z); break;
				case "c9": cubes[i].transform.localScale = new Vector3(x, mapData[8], z); break;
				case "c10": cubes[i].transform.localScale = new Vector3(x, mapData[9], z); break;
				case "c11": cubes[i].transform.localScale = new Vector3(x, mapData[10], z); break;
				case "c12": cubes[i].transform.localScale = new Vector3(x, mapData[11], z); break;
				case "c13": cubes[i].transform.localScale = new Vector3(x, mapData[12], z); break;
				case "c14": cubes[i].transform.localScale = new Vector3(x, mapData[13], z); break;
				case "c15": cubes[i].transform.localScale = new Vector3(x, mapData[14], z); break;
				case "c16": cubes[i].transform.localScale = new Vector3(x, mapData[15], z); break;
				case "c17": cubes[i].transform.localScale = new Vector3(x, mapData[16], z); break;
				case "c18": cubes[i].transform.localScale = new Vector3(x, mapData[17], z); break;
				case "c19": cubes[i].transform.localScale = new Vector3(x, mapData[18], z); break;
				case "c20": cubes[i].transform.localScale = new Vector3(x, mapData[19], z); break;
				case "c21": cubes[i].transform.localScale = new Vector3(x, mapData[20], z); break;
				case "c22": cubes[i].transform.localScale = new Vector3(x, mapData[21], z); break;
			}
		}
	}
	
	void OnApplicationQuit()
	{
		BASS_StreamFree(stream);
		BASS_Free();
	}

	
}