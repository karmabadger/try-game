using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNode : System.Object
{
   private int Xprev;
   private int Yprev;

   private int Xaft;
   private int Yaft;

   private int XPos;
   private int YPos;

   private bool isUsed;

   private int blockType;

   public MazeNode(int xPos, int yPos, bool isUsed)
   {
      XPos = xPos;
      YPos = yPos;
      this.isUsed = isUsed;
   }

   public int XPos1
   {
      get => XPos;
      set => XPos = value;
   }

   public int YPos1
   {
      get => YPos;
      set => YPos = value;
   }

   public bool IsUsed
   {
      get => isUsed;
      set => isUsed = value;
   }

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
