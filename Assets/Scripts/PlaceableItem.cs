using UnityEngine;

/// <summary>
/// Компонент, который должен быть на каждом префабе предмета.
/// Хранит размер в клетках, состояние превью/установленности и методы для визуальных эффектов.
/// </summary>

public class PlaceableItem : MonoBehaviour
{
    [Header("Size in cells (when rotation = 0)")]
    public int sizeX = 1;
    public int sizeY = 1;

    [Header("Visual settings")]
    public float previewAlpha = 0.6f;
    public float placedAlpha = 1f;
    public float liftY = 0.15f; // как сильно предмет "поднимается" при захвате

    // runtime
    [HideInInspector] public bool IsPlaced = false;
    [HideInInspector] public Vector2Int currentGridPosition = Vector2Int.zero;
    [HideInInspector] public float currentRotationDeg = 0f; // 0 или 90/180/270

    private SpriteRenderer sr;
    private Vector3 originalScale;
    private Color originalColor;

    // цвета для превью (можно менять)
    public static readonly Color PreviewColorOK = new Color(1f, 1f, 1f, 0.65f);
    public static readonly Color PreviewColorBad = new Color(1f, 0.4f, 0.4f, 0.65f);

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        originalColor = sr.color;
    }


    public void SetAsPreview(bool preview)
    {
        if (preview)
        {
            // визуально: полупрозрачный и "поднят"
            SetPreviewColor(PreviewColorOK);
            transform.localScale = originalScale;
            transform.position += Vector3.up * liftY;
            IsPlaced = false;
        }
        else
        {
            // установлен / не превью
            sr.color = originalColor;
            transform.localScale = originalScale;
            transform.position += Vector3.down * liftY; // вернуть вниз
            IsPlaced = true;
        }
    }

    /// <summary>
    /// Установить цвет для превью (красный/зелёный)
    /// </summary>
    public void SetPreviewColor(Color c)
    {
        if (sr != null) sr.color = c;
    }

    /// <summary>
    /// Повернуть на +90 градусов.
    /// Важно: размеры переключаются местами.
    /// </summary>
    public void Rotate90()
    {
        currentRotationDeg = (currentRotationDeg + 90f) % 360f;
        transform.rotation = Quaternion.Euler(0f, 0f, currentRotationDeg);

        // поменяем логику размеров (в PlacementManager будет учитываться через GetCurrentWidth/GetCurrentHeight)
        // здесь ничего не нужно менять, т.к. размеры читаются методами ниже
    }

    public int GetCurrentWidth()
    {
        bool rotated = Mathf.Abs((int)currentRotationDeg % 180) == 90;
        return rotated ? sizeY : sizeX;
    }

    public int GetCurrentHeight()
    {
        bool rotated = Mathf.Abs((int)currentRotationDeg % 180) == 90;
        return rotated ? sizeX : sizeY;
    }
}
