using Alien.UI.States;
using AutoMapper;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class InGameViewModel : ViewModelBase
    {
        public InGameViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper)
            : base(regionNavigationService, authenticator, mapper)
        {

        }

        public void ThrowDices(int attributeValue, int competenceValue, int bonus, int malus, int stressValue)
        {
            int[] normalDices;
            int[] stressDices = new int[stressValue];

            if ((attributeValue + competenceValue + bonus) < malus)
            {
                normalDices = new int[0];
            }
            else
            {
                normalDices = new int[attributeValue + competenceValue + bonus];
            }

            for (int i = 0; i < normalDices.Length; i++)
            {
                normalDices[i] = new Random().Next(1, 6);
            }

            for (int i = 0; i < stressDices.Length; i++)
            {
                stressDices[i] = new Random().Next(1, 6);
            }
        }
    }
}
