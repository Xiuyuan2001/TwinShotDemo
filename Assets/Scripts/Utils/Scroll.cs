using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 2f;                      // �����ٶȡ���ֵ�������
    public bool scrollWithGameSpeed = true;             // �Ƿ���Time.timeScaleӰ��

    private float _spriteWidth;                         // ��������ͼ�Ŀ��
    private Transform[] _backgroundPieces;              // �����ӱ���������
    private int _leftIndex;                             // ����߱���������
    private int _rightIndex;                            // ���ұ߱���������

    void Start()
    {
        // ��ȡ�����ӱ�����Transform
        _backgroundPieces = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _backgroundPieces[i] = transform.GetChild(i);
        }

        // ��X��������ȷ����ʼ��������˳��
        SortBackgroundPiecesByPosition();

        // ���һ�ű���ͼ�Ŀ��
        SpriteRenderer spriteRenderer = _backgroundPieces[0].GetComponent<SpriteRenderer>();
        _spriteWidth = spriteRenderer.bounds.size.x;

        // ���������ʼ������
        _leftIndex = 0;
        _rightIndex = _backgroundPieces.Length - 1;
    }

    void Update()
    {
        // ���㱾֡���ƶ���
        float moveAmount = scrollSpeed * (scrollWithGameSpeed ? Time.deltaTime : Time.unscaledDeltaTime);

        // �ƶ����б���������
        foreach (Transform piece in _backgroundPieces)
        {
            piece.Translate(Vector3.left * moveAmount);
        }

        // ��鲢ִ��ѭ���߼�
        CheckAndReposition();
    }

    // ð�� - ��X�������С���󣨴����ң����򱳾���
    private void SortBackgroundPiecesByPosition()
    {
        for (int i = 0; i < _backgroundPieces.Length - 1; i++)
        {
            for (int j = 0; j < _backgroundPieces.Length - i - 1; j++)
            {
                if (_backgroundPieces[j].position.x > _backgroundPieces[j + 1].position.x)
                {
                    Transform temp = _backgroundPieces[j];
                    _backgroundPieces[j] = _backgroundPieces[j + 1];
                    _backgroundPieces[j + 1] = temp;
                }
            }
        }
    }

    // �������ߵ�ͼ�Ƿ��Ѿ���ȫ�Ƴ���Ļ�����������ŵ����ұ�
    private void CheckAndReposition()
    {
        Transform leftMostPiece = _backgroundPieces[_leftIndex];
        Transform rightMostPiece = _backgroundPieces[_rightIndex];

        // �������ߵı����Ƿ��Ѿ���ȫ�Ƴ������Ұ���
        if (leftMostPiece.position.x + _spriteWidth / 2 < GetScreenLeftEdge())
        {
            // �����µ�λ�ã������ŵ���ǰ���ұ߱������ұ�
            Vector3 newPosition = rightMostPiece.position;
            newPosition.x += _spriteWidth;

            leftMostPiece.position = newPosition;

            // ��������
            SortBackgroundPiecesByPosition();

        }
    }

    // ��ȡ��Ļ���Ե����������
    private float GetScreenLeftEdge()
    {
        // ����Ļ���½�(0,0)ת��Ϊ��������
        return Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
    }
}