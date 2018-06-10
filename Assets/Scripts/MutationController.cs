using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationController : MonoBehaviour {

    public struct PlayerMutation
    {
        public float speedModifier;
        public float cadencyModifier;
        public int lifeModifier;
    }

    public struct BulletMutation
    {
        public float bulletRangeModifier;
        public float bulletSpeedModificer;
        public float bulletDamageModifier;
    }

    public PlayerMutation playerMutation;
    public BulletMutation bulletMutation;


    public enum MutationType
    {
        GOOD,
        BAD,
        UNKNOWN
    }

    public enum AffectingType
    {
        ALL,
        ONE,
        TWO
    }

    public float probabilityGood;
    public float probabilityBad;
    public float probabilityUnknown;
    public MutationType mutationType;

    public float probabilityAll;
    public float probabilityOne;
    public float probabilityTwo;
    public AffectingType affectingType;

    // Use this for initialization
    void Start() {

        playerMutation = new PlayerMutation();
        bulletMutation = new BulletMutation();

        #region --initProbabilities--

        float value = Random.value;
        if (value <= probabilityGood)
        {
            mutationType = MutationType.GOOD;
            /* URI: Change shader color (green) */
        }
        else if (value > probabilityGood && value < probabilityGood + probabilityBad)
        {
            mutationType = MutationType.BAD;
            /* URI: Change shader color (red) */
        }
        else
        {
            mutationType = MutationType.UNKNOWN;
            /* URI: Change shader color (grey¿?) */
        }

        value = Random.value;
        if (value <= probabilityAll)
        {
            affectingType = AffectingType.ALL;
        }
        else if (value > probabilityAll && value < probabilityAll + probabilityOne)
        {
            affectingType = AffectingType.ONE;
        }
        else
        {
            affectingType = AffectingType.TWO;
        }
        #endregion --initProbabilities--

        #region --initStruct--
        switch (affectingType)
        {
            case AffectingType.ALL:
                SetSpeedModifier();
                SetCadency();
                SetLifeModifier();
                SetBulletRangeModifier();
                SetBulletSpeedModificer();
                SetBulletDamageModifier();
                break;
            case AffectingType.ONE:
                int attributeValue = Random.Range(0, 5);
                if (attributeValue == 0)
                {
                    SetSpeedModifier();
                }
                else if (attributeValue == 1)
                {
                    SetCadency();

                }
                else if (attributeValue == 2)
                {
                    SetLifeModifier();

                }
                else if (attributeValue == 3)
                {
                    SetBulletRangeModifier();

                }
                else if (attributeValue == 4)
                {
                    SetBulletSpeedModificer();

                }
                else if (attributeValue == 5)
                {
                    SetBulletDamageModifier();
                }
                break;
            case AffectingType.TWO:
                for (int i = 0; i < 2; i++)
                {
                    int attributeValue1 = Random.Range(0, 5);
                    if (attributeValue1 == 0)
                    {
                        SetSpeedModifier();
                    }
                    if (attributeValue1 == 1)
                    {
                        SetCadency();

                    }
                    if (attributeValue1 == 2)
                    {
                        SetLifeModifier();

                    }
                    if (attributeValue1 == 3)
                    {
                        SetBulletRangeModifier();

                    }
                    if (attributeValue1 == 4)
                    {
                        SetBulletSpeedModificer();

                    }
                    if (attributeValue1 == 5)
                    {
                        SetBulletDamageModifier();
                    }
                }
                break;
        }
        #endregion --initStruct--

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Sending my type " + mutationType + " and " + affectingType);
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.SetModifier(playerMutation, bulletMutation);
            Destroy(this.gameObject);
        }
    }

    #region --Setters modifiers--
    private void SetSpeedModifier()
    {
        switch (mutationType)
        {
            case MutationType.GOOD:
                playerMutation.speedModifier = Random.Range(0.1f, 0.3f);
                break;
            case MutationType.BAD:
                playerMutation.speedModifier = Random.Range(-0.3f, -0.1f);
                break;
            case MutationType.UNKNOWN:
                playerMutation.speedModifier = Random.Range(-0.5f, 0.6f);
                break;
        }
    }
    private void SetCadency() {
        switch (mutationType)
        {
            case MutationType.GOOD:
                playerMutation.cadencyModifier = Random.Range(-0.2f, -0.05f);
                break;
            case MutationType.BAD:
                playerMutation.cadencyModifier = Random.Range(0.05f, 0.2f);
                break;
            case MutationType.UNKNOWN:
                playerMutation.cadencyModifier = Random.Range(-0.2f, 0.15f);
                break;
        }
    }
    private void SetLifeModifier() {
        switch (mutationType)
        {
            case MutationType.GOOD:
                playerMutation.lifeModifier = Random.Range(5, 10);
                break;
            case MutationType.BAD:
                playerMutation.lifeModifier = Random.Range(-10, -5);
                break;
            case MutationType.UNKNOWN:
                playerMutation.lifeModifier = Random.Range(-10, 15);
                break;
        }
    }
    private void SetBulletRangeModifier() {
        switch (mutationType)
        {
            case MutationType.GOOD:
                bulletMutation.bulletRangeModifier = Random.Range(1f, 3f);
                break;
            case MutationType.BAD:
                bulletMutation.bulletRangeModifier = Random.Range(-3f, -1f);
                break;
            case MutationType.UNKNOWN:
                bulletMutation.bulletRangeModifier = Random.Range(-3f, 3.5f);
                break;
        }
    }
    private void SetBulletSpeedModificer() {
        switch (mutationType)
        {
            case MutationType.GOOD:
                bulletMutation.bulletSpeedModificer = Random.Range(1, 3);
                break;
            case MutationType.BAD:
                bulletMutation.bulletSpeedModificer = Random.Range(-3f, -1f);
                break;
            case MutationType.UNKNOWN:
                bulletMutation.bulletSpeedModificer = Random.Range(-3f, 3.5f);
                break;
        }
    }
    private void SetBulletDamageModifier() {
        switch (mutationType)
        {
            case MutationType.GOOD:
                bulletMutation.bulletDamageModifier = Random.Range(1, 3);
                break;
            case MutationType.BAD:
                bulletMutation.bulletDamageModifier = Random.Range(-3, -1);
                break;
            case MutationType.UNKNOWN:
                bulletMutation.bulletDamageModifier = Random.Range(-3, 4);
                break;
        }
    }
    #endregion --Setters modifiers--
}