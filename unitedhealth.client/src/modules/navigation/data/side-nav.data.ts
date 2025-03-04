import { SideNavItems, SideNavSection } from '@modules/navigation/models';

export const sideNavSections: SideNavSection[] = [
    {
        text: 'MENU',
        items: ['dashboard', 'personalTrainer', 'personalTrainerPatient', 'medical', 'nutrition', 'nutritionPatient'],
    }
];

export const sideNavItems: SideNavItems = {
    dashboard: {
        icon: 'tachometer-alt',
        text: 'Dashboard',
        link: '/dashboard',
    },
    personalTrainer: {
        icon: 'dumbbell',
        text: 'Personal Trainer',
        isVisible:["Training"],
        submenu: [
            {
                icon: 'cog',
                text: 'Personal profile',
                link: '/personal-trainer/personal-profile'
            },
            {
                icon: 'stethoscope',
                text: 'Consultations',
                link: '/personal-trainer/consultas'
            },
            {
                icon: 'user',
                text: 'Patients',
                link: '/personal-trainer/pacientes'
            }

        ]
    },
    personalTrainerPatient: {
        icon: 'dumbbell',
        text: 'Personal Trainer',
        isVisible:["Patient"],
        link: '/personal-trainer',
    },
    medical: {
        icon: 'file-medical',
        text: 'Medical',
        isVisible:["Patient", "Medical"],
        link: '/medical'
    },
    nutrition: {
        icon: 'utensils',
        text: 'Nutrition',
        isVisible: ["Nutrition"],
        submenu: [
            {
                icon: 'cog',
                text: 'Nutritionist profile',
                link: '/nutrition/nutritionist-profile'
            },
            {
                icon: 'stethoscope',
                text: 'Consultas',
                link: '/nutrition/appointments'   
            },
            {
                icon: 'user',
                text: 'Pacientes',
                link: '/nutrition/patients'   
            }
        ]
    },
    nutritionPatient: {
        icon: 'utensils',
        text: 'Nutrition',
        link: '/nutrition',
        isVisible: ["Patient"]
    }
};
