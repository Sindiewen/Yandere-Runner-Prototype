using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.InfiniteRunnerEngine
{
	/// <summary>
	/// A class used to display the achievements on screen. 
	/// The AchievementDisplayItems will be parented to it, so it's better if it has a LayoutGroup (Vertical or Horizontal) too.
	/// </summary>
	public class MMAchievementDisplayer : MonoBehaviour, MMEventListener<MMAchievementUnlockedEvent>
	{
		[Header("Achievements")]
		/// the prefab to use to display achievements
		public MMAchievementDisplayItem AchievementDisplayPrefab;
		/// the duration the achievement will remain on screen for when unlocked
		public float AchievementDisplayDuration = 5f;
		/// the fade in/out speed
		public float AchievementFadeDuration = 0.2f;

		public virtual IEnumerator DisplayAchievement(MMAchievement achievement)
		{
			if ((this.transform == null) || (AchievementDisplayPrefab == null))
			{
				yield break;
			}

			// we instantiate our achievement display prefab, and add it to the group that will automatically handle its position
			GameObject instance = (GameObject)Instantiate(AchievementDisplayPrefab.gameObject);
			instance.transform.SetParent(this.transform,false);

			// we get the achievement displayer
			MMAchievementDisplayItem achievementDisplay = instance.GetComponent<MMAchievementDisplayItem> ();
			if (achievementDisplay == null)
			{
				yield break;
			}

			// we fill our achievement
			achievementDisplay.Title.text = achievement.Title;
			achievementDisplay.Description.text = achievement.Description;
			achievementDisplay.Icon.sprite = achievement.UnlockedImage;
			if (achievement.AchievementType == AchievementTypes.Progress)
			{
				achievementDisplay.ProgressBarDisplay.gameObject.SetActive(true);
			}
			else
			{
				achievementDisplay.ProgressBarDisplay.gameObject.SetActive(false);
			}

			// we play a sound if set
			if (achievement.UnlockedSound != null)
			{
				SoundManager.Instance.PlaySound (achievement.UnlockedSound,Vector3.zero);
			}

			// we fade it in and out
			CanvasGroup achievementCanvasGroup = instance.GetComponent<CanvasGroup> ();
			if (achievementCanvasGroup != null)
			{
				achievementCanvasGroup.alpha = 0;
				StartCoroutine(MMFade.FadeCanvasGroup(achievementCanvasGroup, AchievementFadeDuration, 1));
				yield return new WaitForSeconds(AchievementFadeDuration + AchievementDisplayDuration);
				StartCoroutine(MMFade.FadeCanvasGroup(achievementCanvasGroup, AchievementFadeDuration, 0));
			}
		}

		/// <summary>
		/// When an achievement is unlocked, we display it
		/// </summary>
		/// <param name="achievementUnlockedEvent">Achievement unlocked event.</param>
		public virtual void OnMMEvent(MMAchievementUnlockedEvent achievementUnlockedEvent)
		{
			StartCoroutine(DisplayAchievement (achievementUnlockedEvent.Achievement));
		}

		/// <summary>
		/// On enable, we start listening for unlocked achievements
		/// </summary>
		protected virtual void OnEnable()
		{
			this.MMEventStartListening<MMAchievementUnlockedEvent>();
		}

		/// <summary>
		/// On disable, we stop listening for unlocked achievements
		/// </summary>
		protected virtual void OnDisable()
		{
			this.MMEventStopListening<MMAchievementUnlockedEvent>();
		}
	}
}