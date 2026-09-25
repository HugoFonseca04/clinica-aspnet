using System.ComponentModel.DataAnnotations;

namespace ClinicaASPNet.Models
{
    public class Especialidade
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve possuir entre 3 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "A descrição deve possuir entre 5 e 300 caracteres")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100)]
        [Display(Name = "Área de Atuação")]
        public string AreaAtuacao { get; set; } = string.Empty;

        [Range(10, 240, ErrorMessage = "Informe uma duração entre 10 e 240 minutos")]
        [Display(Name = "Duração da Consulta (minutos)")]
        public int DuracaoConsulta { get; set; }

        [Display(Name = "Ativa")]
        public bool Ativa { get; set; } = true;

        public DateTime CriadoEm { get; set; }

        public ICollection<Profissional> Profissionais { get; set; } = new List<Profissional>();
    }
}
