using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Menu
{
    public class MenuContext : MonoBehaviour
    {
        [SerializeField] private List<MenuChild> children = null;
        [SerializeField] private int row = 1;//ˆÚ“®‚·‚és”
        [SerializeField] private int column = 1; //ˆÚ“®‚·‚é—ñ”
        [SerializeField] private bool repeatMove = true;

        [SerializeField] private UnityEvent selectedAction = null;
        [SerializeField] private UnityEvent diselectedAction = null;

        public int SelectedIdx { get; private set; } = 0;
        public int ChildCount => children.Count;

        public void Active()
        {
            selectedAction?.Invoke();
            Select(SelectedIdx);
        }

        public void Inactive()
        {
            children[SelectedIdx]?.Diselect();
            diselectedAction?.Invoke();
        }

        public void Select(int i)
        {
            if(children.Count > SelectedIdx) children[SelectedIdx]?.Diselect();

            SelectedIdx = children.Count > i ? SelectedIdx = i : SelectedIdx = 0;
            children[SelectedIdx].Select();
        }

        public void Move(Direction dir)
        {
            int move = DirToInt(dir);
            int moved = SelectedIdx + move;

            if (moved < 0)
            {
                if (repeatMove) 
                    moved = children.Count - 1;
                else 
                    moved = 0;
            }
            else
            if(moved >= children.Count)
            {
                if (repeatMove) 
                    moved = 0;
                else 
                    moved = children.Count - 1;
            }
            Select(moved);
        }

        public void Decided()
        {
            children[SelectedIdx].Decide();
        }

        public void Canceled()
        {
            children[SelectedIdx].Cancel();
        }

        private int DirToInt(Direction dir)
        {
            switch (dir)
            {
                case Direction.Left:
                    return -row;
                case Direction.Right:
                    return row;
                case Direction.Down:
                    return column;
                case Direction.Up:
                    return -column;
                default:
                    return 0;
            }
        }

        public void AddChild(MenuChild child)
        {
            children.Add(child);
        }

        public void InsertChild(int idx, MenuChild child)
        {
            children.Insert(idx, child);
            if(SelectedIdx >= idx) SelectedIdx++;
        }

        public void RemoveChild(int idx)
        {
            children.RemoveAt(idx);
            if (SelectedIdx >= idx) Select(idx);
        }

        public void ClearChild()
        {
            children.Clear();
        }
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}
