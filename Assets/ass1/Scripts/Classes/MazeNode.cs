using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNode : System.Object
{
   private int Xprev;
   private int Yprev;

   private int Xaft;
   private int Yaft;


   private int blockType;

   public MazeNode()
   {
   }

   public int BlockType
   {
      get => blockType;
      set => blockType = value;
   }

   public int Xprev2
   {
      get => Xprev;
      set => Xprev = value;
   }

   public int Yprev2
   {
      get => Yprev;
      set => Yprev = value;
   }

   public int Xaft2
   {
      get => Xaft;
      set => Xaft = value;
   }

   public int Yaft2
   {
      get => Yaft;
      set => Yaft = value;
   }
   
}
