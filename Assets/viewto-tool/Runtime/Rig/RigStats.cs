using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ViewToUnity {
    
 
    public class RigStats {

        public enum Stage {

            Target,
            Blocker,
            Design,
            Complete

        }

        private readonly List<int> _frameRates = new List<int>( );
        private const int FrameRateCount = 10;

        public int GetRate {
            get
            {
                int frame = (int) Math.Floor( 1f / Time.unscaledDeltaTime );

                if ( _frameRates.Count >= FrameRateCount )
                    _frameRates.RemoveAt( 0 );

                _frameRates.Add( frame );

                int smoothedFrameCount = _frameRates.Sum( );

                smoothedFrameCount /= _frameRates.Count;
                return smoothedFrameCount;
            }
        }

    }
}