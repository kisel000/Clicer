using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Numerics;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace ClicerUWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // кол-во очков.
        BigInteger score = new BigInteger(0);
        // уровень умений.
        BigInteger skill = new BigInteger(1);
        // порог.
        BigInteger target = new BigInteger(10);
        // сколько прибовляется очков в секунду.
        BigInteger skill2 = new BigInteger(0);
        // цены на скиллы.
        BigInteger[] prices =
         {
            100,
            200
        };
        // купил ли пользователь скилл.
        bool isUserBuySkill = false;


        DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();
            // выводим кол-во очков.
            scoreValue.Text = score.ToString();
            // выводим скилл.
            skillValue.Text = skill.ToString();
            // выводим цену.
            skillPriceText.Text = prices[0].ToString();

            skill2PriceText.Text = prices[1].ToString();
            // блокируем кнопку.
            buySkills.IsEnabled = false;

            buySkillButton.IsEnabled = false;

            timer = new DispatcherTimer();

            timer.Interval = new TimeSpan(0, 0, 1);

            timer.Tick += Timer_Tick;

            timer.Start();
        }
        public void Timer_Tick(object s, object e)
        {
            if (isUserBuySkill)
            {
                score += skill2;
                scoreValue.Text = score.ToString();
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            // если мы переступили порог, то...
            if (score >= target)
            {
                // увеличиваем скилл на 1.
                skill = BigInteger.Add(skill, 1);
                // поднимаем порог. 
                target = BigInteger.Multiply(target, 2);
            }

            // обновляем значение очков.
            score = BigInteger.Add(score, skill);
            // обновляем кол-во очков.
            scoreValue.Text = score.ToString();
            // обновляем скилл.
            skillValue.Text = skill.ToString();
            // обновляем  цену.
            skillPriceText.Text = prices[0].ToString();

            if (score >= prices[0]) buySkills.IsEnabled = true;

            if (score >= prices[1]) buySkillButton.IsEnabled = true;
            else buySkillButton.IsEnabled = false;
        }

        private void buySkills_Click(object sender, RoutedEventArgs e)
        {
            // если нам хватает очков, то...
            if (score >= prices[0])
            {
                // вычитаем очки.
                score = BigInteger.Subtract(score, prices[0]);
                // зачисляем скиллы.
                skill = BigInteger.Add(skill, 5);
                // увеличиваем цену.
                prices[0] = BigInteger.Multiply(prices[0], 50);
                // обновляем кол-во очков.
                scoreValue.Text = score.ToString();
                // обновляем скилл.
                skillValue.Text = skill.ToString();
                // обновляем цену.
                skillPriceText.Text = prices[0].ToString();
                // блокируем кнопку. 
                buySkills.IsEnabled = false;
            }
        }

        private void buySkillButton_Click(object sender, RoutedEventArgs e)
        {
            if (score >= prices[1])
            {
                score -= prices[1];
                skill2 += 5;
                prices[1] *= 2;
                isUserBuySkill = true;
                skill2PriceText.Text = prices[1].ToString();
            }
        }
    }
}
