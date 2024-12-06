using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   public enum WallOrrientation
    {
        WEST=0, NORTH=1, FAST=2,SOUTH=3
    }

    public class Cell: MonoBehaviour
    {
        public GameObject[] walls;
        public bool isVisited=false;
        private bool[] wallsActive = { true, true, true, true };

        public void HideWall(WallOrrientation orientation)
        {
            int index= (int)orientation;

            if (walls[index]!=null)
            {
                Destroy(walls[index]);
                this.walls[index] = null;
                this.wallsActive[index] = false ;
            }
        } 

        public bool isWallActive(WallOrrientation orientation)
        {
            int index=(int)orientation;
            return this.wallsActive[index];
        }
    }

