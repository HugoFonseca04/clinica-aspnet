using System.ComponentModel.DataAnnotations;

namespace ClinicaASPNet.Models
{
    public class Profissional
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, MinimumLength = 3)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(30)]
        [Display(Name = "Registro Profissional")]
        public string RegistroProfissional { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [Phone(ErrorMessage = "Informe um telefone válido")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;

        [Required(ErrorMessage = "Selecione uma especialidade")]
        [Display(Name = "Especialidade")]
        public int EspecialidadeId { get; set; }

        public Especialidade? Especialidade { get; set; }

        public DateTime CriadoEm { get; set; }
    }
}
