﻿using UnityEngine;
using System.Collections;

namespace Audial.Utils{
	enum RunState {Running, Paused, Stopped};
	public class LFO{

		private int tableLength = 128;
		public static float[] waveTable;

		private float _index = 0;
		public float Index {
			get{
				return _index;
			}
			set{
				_index = value;
				if(_index>=tableLength-0.5f){
					_index -= tableLength;
				}
			}
		}

		RunState runState = RunState.Running;
		public IEnumerator Run(){
			runState = RunState.Running;
			while(runState != RunState.Stopped){
				if(runState == RunState.Running){
					Index += tableLength / StepSize * Time.deltaTime;
				}
				yield return new WaitForSeconds(0.002f);
			}
		}

		public void Pause(){
			runState = RunState.Paused;
		}

		public void Resume(){
			runState = RunState.Running;
		}

		public void Stop(){
			runState = RunState.Stopped;
		}

		private float _stepSize = 0.3f;
		public float StepSize{
			get{
				return _stepSize;
			}
			set{
				_stepSize = value;
			}
		}

		public void SetRate(float rate){
			StepSize = rate;
		}

		public int GetIndex(){
			return Mathf.RoundToInt(Index)%waveTable.Length;
		}

		public float GetValue(){
			return waveTable[GetIndex()];
		}

		public void MoveIndex(){
			Index += tableLength / StepSize / Audial.Utils.Settings.SampleRate;
		}

		public float[] GetChunkValue(int chunkLength){
			float[] retVal = new float[chunkLength];
			for(var i = 0; i < chunkLength; i++){
				retVal[i] = GetValue();
			}
			return retVal;
		}

		public LFO(){
			if(waveTable==null){
				CreateWavetable();
			}
		}

		public LFO(float speed){
			if(waveTable==null){
				CreateWavetable();
			}
			StepSize = speed;
		}

		private void CreateWavetable(){
			waveTable = new float[tableLength];
			for(var i = 0; i < tableLength; i++){
				waveTable[i] = 0.5f + (Mathf.Sin(2*Mathf.PI * i / tableLength)/2);
			}
		}
		
	}
}
