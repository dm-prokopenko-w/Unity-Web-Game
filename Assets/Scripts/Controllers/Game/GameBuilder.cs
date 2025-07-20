using System.Collections.Generic;
using System.Linq;
using Configs;
using Controllers.SceneTransition;
using DG.Tweening;
using Transfer.ApiData;
using UnityEngine;
using Zenject;

public class GameBuilder : IGameBuilder
{
    [Inject] private readonly GridConfig _gridConfig;
    [Inject] private readonly ISceneTransitionController _sceneTransition;

    private List<ButtonGrid> _buttonsGrid = new();
    private List<Tier> _tiers = new();
    private int _progress, _numKeys;
    private KeyPanelView _keyFly;

    private float Progress => (1f / (_numKeys + 1)) * _progress;

    public void Setup(SessionData sessionData, GameView view)
    {
        _progress = sessionData.progress;
        _numKeys = _gridConfig.GetGrid(sessionData.tier).numKeys;
        view.Setup(Progress, _gridConfig.GetGrid(sessionData.tier).size.x, Claim);
        GridBuild(view, sessionData);
        SetRandomKey(sessionData.seed, _numKeys - sessionData.progress);
        ChestBuild(view, _numKeys);
        KeyBuild(view, _numKeys);
        view.SetBlock(false);
    }

    private void Claim()
    {
        Debug.Log("Claim");
    }
    
    private void ChestBuild(GameView view, int numKeys)
    {
        var prefabChest = Resources.Load<ChestPanelView>(Constants.ChestPanelPath);
        if (prefabChest == null)
        {
            Debug.LogError("ChestPanelView component not found");
            return;
        }

        for (int i = 0; i < numKeys; i++)
        {
            var chest = GameObject.Instantiate(prefabChest, view.ChestParent);
            chest.gameObject.name = "ChestPanelView - " + i;
            chest.Setup(_progress > i);

            _tiers.Add(new Tier()
            {
                id = i,
                view = chest
            });
        }
    }

    private void KeyBuild(GameView view, int numKeys)
    {
        var prefabKey = Resources.Load<KeyPanelView>(Constants.KeyPanelPath);

        for (int i = 0; i < numKeys; i++)
        {
            var key = GameObject.Instantiate(prefabKey, view.KeyParent);
            _tiers[i].key = key;
            key.Setup(_progress > i);
        }
    }

    private void GridBuild(GameView view, SessionData sessionData)
    {
        var prefabBtn = Resources.Load<ButtonGridView>(Constants.ButtonGridPath);
        if (prefabBtn == null)
        {
            Debug.LogError("ButtonGridView component not found");
            return;
        }

        for (int y = 0; y < _gridConfig.GetGrid(sessionData.tier).size.y; y++)
        {
            for (int x = 0; x < _gridConfig.GetGrid(sessionData.tier).size.x; x++)
            {
                var btnView = GameObject.Instantiate(prefabBtn, view.Grid.transform);
                btnView.gameObject.name = "ButtonGridView - " + x + "x" + y;
                var btn = new ButtonGrid(x, y, btnView);
                btnView.Setup(() => { OnOpen(view, btn); });
                _buttonsGrid.Add(btn);
            }
        }

        if (sessionData.openedCells != null && sessionData.openedCells.Count > 0)
        {
            foreach (var cell in sessionData.openedCells)
            {
                foreach (var btn in _buttonsGrid)
                {
                    if (cell.x == btn.CellX && cell.y == btn.CellY)
                    {
                        btn.IsOpen = true;
                    }
                }
            }
        }
    }

    private void SetRandomKey(int seed, int count)
    {
        Random.InitState(seed);
        var list = _buttonsGrid.FindAll(x => !x.IsOpen).ToArray();

        list = list.OrderBy(x => Random.value).ToArray();

        var selectedItems = list.Take(count).ToArray();

        foreach (var item in selectedItems)
        {
            item.IsKey = true;
        }
    }

    private void OnOpen(GameView view, ButtonGrid btn)
    {
        if (!btn.IsKey) return;
        view.SetBlock(true);

        if (_keyFly == null)
        {
            var keyPrefab = Resources.Load<KeyPanelView>(Constants.KeyPanelPath);
            _keyFly = GameObject.Instantiate(keyPrefab, view.transform);
            _keyFly.Setup(true);
        }

        _keyFly.gameObject.SetActive(true);
        _keyFly.transform.position = btn.Pos;
        _keyFly.transform.localScale = Vector3.zero;
        _keyFly.transform.rotation = Quaternion.identity;

        var seq = DOTween.Sequence();

        seq.Append(_keyFly.transform.DOMove(Vector3.zero, Constants.appearDuration)
            .SetEase(Ease.OutBack));
        seq.Join(_keyFly.transform.DOScale(Vector3.one * 3f, Constants.appearDuration)
            .SetEase(Ease.OutBack));

        seq.Append(_keyFly.transform.DOMove(_tiers[_progress].key.transform.position, Constants.moveDuration)
            .SetEase(Ease.InQuad));
        seq.Join(_keyFly.transform.DOScale(Vector3.one, Constants.moveDuration)
            .SetEase(Ease.InQuad));
        seq.Join(_keyFly.transform.DORotate(
            new Vector3(0, 0, Constants.rotateSpeed * 2f), Constants.moveDuration, RotateMode.FastBeyond360));

        seq.OnComplete(() =>
        {
            _keyFly.gameObject.SetActive(false);
            _tiers[_progress].key.Setup(true);
            
            if (_progress == _numKeys - 1)
            {
                view.UpdateView(1f);
                Win(view);
            }
            else
            {
                view.SetBlock(false);
                _tiers[_progress].view.SetAnimation(Constants.ChestAnimOnOpen);
                _progress++;
                view.UpdateView(Progress);
            }
        });
    }

    private void Win(GameView view)
    {
        Debug.Log("Win");
        var seq = DOTween.Sequence();
        var chest = _tiers[_progress].view;
        chest.transform.SetParent(view.transform);
        view.Win();

        seq.Append(chest.transform.DOMove(Vector3.zero, Constants.appearDuration).SetEase(Ease.OutBack));
        seq.Join(chest.transform.DOScale(Vector3.one * 2f, Constants.appearDuration).SetEase(Ease.OutBack));

        seq.OnComplete(() =>
        {
            _tiers[_progress].view.SetAnimation(Constants.ChestAnimOnOpen);
            _tiers[_progress].key.Setup(true);
        });
    }
}